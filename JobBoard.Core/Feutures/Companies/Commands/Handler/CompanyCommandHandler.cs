using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
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
										IRequestHandler<DeleteCompanyCommand, Response<string>>
	{
		#region Fields 
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;

		#endregion

		#region constructors
		public CompanyCommandHandler(ICompanyService companyService,
									IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,
									IAuthorizationService authorizationService,
									ICurrentUserService currentUserService
									) : base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
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


			if (!(await _currentUserService.GetCurrentUserRoles()).Contains("Admin"))
			{
				var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), company, new CompanyOwnerRequirement());

				if (!isAuthorized.Succeeded) return Forbidden<int>(_stringLocalizer[SharedResourcesKeys.NoAccess]);
			}


			if (company == null) return NotFound<int>("There is no Company to Ipdate! Make sure to enter the correct Id");

			company = _mapper.Map(request, company);

			await _companyService.UpdateAsync(company);

			return Success(request.CompanyId);
		}

		public async Task<Response<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = await _companyService.GetCompanyByIdAsync(request.Id);

			var userRoles = await _currentUserService.GetCurrentUserRoles();

			if (!userRoles.Contains("Admin"))
			{
				var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), company, new CompanyOwnerRequirement());

				if (!isAuthorized.Succeeded) return Forbidden<string>(_stringLocalizer[SharedResourcesKeys.NoAccess]);

			}

			if (company == null) return BadRequest<string>($"There Is No Country With Id = {request.Id}");

			await _companyService.DeleteAsync(company);

			return Deleted("Deleted");
		}
		#endregion
	}
}
