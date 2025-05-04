using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;
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

		#endregion

		#region constructors
		public CompanyCommandHandler(ICompanyService companyService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
		}
		#endregion

		#region Handlers

		// addNew Handler
		public async Task<Response<int>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = _mapper.Map<Company>(request);

			await _companyService.AddAsync(company);
			return Created(company.CompanyId);
		}

		// update handler
		public async Task<Response<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
		{
			//bool Exist = await _companyService.IsExistByIdAsync(request.CompanyId);
			var company = await _companyService.GetCompanyByIdAsync(request.CompanyId);

			if (company == null) return NotFound<int>("There is no Company to Ipdate! Make sure to enter the correct Id");

			company = _mapper.Map(request, company);

			await _companyService.UpdateAsync(company);

			return Success(request.CompanyId);
		}

		public async Task<Response<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = await _companyService.GetCompanyByIdAsync(request.Id);
			if (company == null) return BadRequest<string>($"There Is No Country With Id = {request.Id}");

			await _companyService.DeleteAsync(company);

			return Deleted("Deleted");
		}
		#endregion
	}
}
