using System.Linq.Expressions;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Companies.Queries.Handler
{
	public class CompanyQueryHandler : ResponseHandler, IRequestHandler<GetSingleCompanyQuery, Response<GetSingleCompanyQueryResponse>>,
														IRequestHandler<GetAllCompaiesQuery, PaginatedResponse<List<GetListCompaniesQueryesponse>>>,
														IRequestHandler<GetPaginatedListCompanyQuery, PaginatedResponse<List<GetPaginatedListCompaniesQueryResponse>>>
	{
		#region Fields
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;
		#endregion

		#region Constructors
		public CompanyQueryHandler(ICompanyService companyService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
		}
		#endregion

		#region Handlers
		public async Task<Response<GetSingleCompanyQueryResponse>> Handle(GetSingleCompanyQuery request, CancellationToken cancellationToken)
		{
			var company = await _companyService.GetCompanyByIdAsync(request.Id);
			if (company == null) return NotFound<GetSingleCompanyQueryResponse>($"No Company With Id = {request.Id}");

			var companyResponse = _mapper.Map<GetSingleCompanyQueryResponse>(company);
			return Success(companyResponse);
		}

		public async Task<PaginatedResponse<List<GetListCompaniesQueryesponse>>> Handle(GetAllCompaiesQuery request, CancellationToken cancellationToken)
		{
			var queryable = _companyService.GetCompaniesQueryable(request.Search, request.Sort);

			return (await _mapper.ProjectTo<GetListCompaniesQueryesponse>(queryable).ToPaginatedAsync(request.Page, request.PageSize));
		}

		public async Task<PaginatedResponse<List<GetPaginatedListCompaniesQueryResponse>>> Handle(GetPaginatedListCompanyQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<Company, GetPaginatedListCompaniesQueryResponse>>
					expression = comp => new GetPaginatedListCompaniesQueryResponse(comp.CompanyId, comp.CompanyName, comp.Description,
													comp.WebsiteUrl, comp.Location, comp.PhoneNumber,
													comp.Email, comp.Fax);

			var queryable = _companyService.FilterPaginatedQueryable(request.Order);

			var result = await queryable.Select(expression).ToPaginatedAsync(request.PageNumber, request.PageSize);

			return result;
		}

		#endregion

	}
}
