using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Companies.Commands.Handler
{
	public class CompanyCommandHandler : ResponseHandler, IRequestHandler<AddCompanyCommand, Response<int>>,
										IRequestHandler<UpdateCompanyCommand, Response<int>>,
										IRequestHandler<DeleteCompanyCommand, Response<string>>,
										IRequestHandler<SetCompanyLogoCommand, Response<string>>
	{
		#region Fields 
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMediator _mediator;

		#endregion

		#region constructors
		public CompanyCommandHandler(ICompanyService companyService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,
									IAuthorizationService authorizationService,
									ICurrentUserService currentUserService,
									IMediator mediator
									) : base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
			_mediator = mediator;
		}
		#endregion

		#region Handlers

		// addNew Handler
		public async Task<Response<int>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = _mapper.Map<Company>(request);

			company.CreatedByUserId = _currentUserService.GetCurrentUserId();
			await _companyService.AddAsync(company);
			return Created(company.CompanyId);
		}

		// update handler
		public async Task<Response<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
		{

			var company = await _companyService.GetCompanyByIdAsync(request.CompanyId);

			if (company == null) return NotFound<int>("There is no Company to Ipdate! Make sure to enter the correct Id");

			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				company,
				AuthorizationPolicies.IsCompanyCreator);

			if (!isAuthorized.Succeeded)
				return Forbidden<int>(_stringLocalizer[SharedResourcesKeys.NoAccess]);


			company = _mapper.Map(request, company);

			await _companyService.UpdateAsync(company);

			return Success(request.CompanyId);
		}

		public async Task<Response<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = await _companyService.GetCompanyByIdAsync(request.Id);

			if (company == null) return BadRequest<string>($"There Is No Company With Id = {request.Id}");

			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				company,
				AuthorizationPolicies.IsCompanyCreator);

			if (!isAuthorized.Succeeded)
				return Forbidden<string>(_stringLocalizer[SharedResourcesKeys.NoAccess]);


			await _companyService.DeleteAsync(company);

			return Deleted("Deleted");
		}

		public async Task<Response<string>> Handle(SetCompanyLogoCommand request, CancellationToken cancellationToken)
		{
			if (request.Logo is null)
				return BadRequest("Logo file is required");

			var company = await _companyService.GetCompanyByIdAsync(request.CompanyId);

			if (company is null) return BadRequest("Target company not found");

			var result = await _mediator.Send(new UploadFileCommand(
				request.Logo,
				Data.enums.FileOwnerType.Companies,
				request.CompanyId,
				Data.enums.FileVisibility.Public,
				Data.enums.FilePathType.UuidFileName));

			if (!result.succeeded)
			{
				return new Response<string>
				{
					statusCode = result.statusCode,
					succeeded = false,
					message = result.message
				};
			}

			company.LogoFileId = result.data;
			await _companyService.UpdateAsync(company);

			return Success("Company logo updated successfully");
		}
		#endregion
	}
}
