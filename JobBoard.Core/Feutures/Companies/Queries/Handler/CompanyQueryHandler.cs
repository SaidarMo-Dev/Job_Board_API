using System.Linq.Expressions;
using System.Reflection;
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
	public class CompanyQueryHandler : ResponseHandler,
						IRequestHandler<GetSingleCompanyQuery, Response<object>>,
						IRequestHandler<GetAllCompaiesQuery, PaginatedResponse<List<GetListCompaniesQueryesponse>>>,
						IRequestHandler<GetPaginatedListCompanyQuery, PaginatedResponse<List<GetPaginatedListCompaniesQueryResponse>>>,
						IRequestHandler<GetPopularCompaniesQuery, Response<string[]>>,
						IRequestHandler<GetCompaniesSummaryQuery, PaginatedResponse<List<GetCompaniesSummaryQueryResponse>>>
	{
		#region Fields
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;

		private static readonly List<PropertyInfo> _cachedCompanyProperties =
	typeof(GetSingleCompanyQueryResponse).GetProperties().ToList();

		#endregion

		#region Constructors
		public CompanyQueryHandler(ICompanyService companyService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
		}
		#endregion

		#region Handlers
		public async Task<Response<object>> Handle(GetSingleCompanyQuery request, CancellationToken cancellationToken)
		{

			var company = await _companyService.GetCompanyByIdAsync(request.Id);
			if (company == null) return NotFound<object>($"No Company With Id = {request.Id}");



			var fullCompanyResponse = _mapper.Map<GetSingleCompanyQueryResponse>(company);

			if (!string.IsNullOrEmpty(request.Fields))
			{
				var partialResponse = new Dictionary<string, object?>();

				var fieldsSet = request.Fields.Split(',')
												.Select(f => f.Trim()).Where(f => !string.IsNullOrEmpty(f)).ToHashSet(StringComparer.InvariantCultureIgnoreCase);

				// Validate fields
				var invalidFields = fieldsSet.Where(f =>
					!_cachedCompanyProperties.Any(p =>
						p.Name.Equals(f, StringComparison.InvariantCultureIgnoreCase))
				).ToList();

				if (invalidFields.Any())
				{
					return BadRequest<object>($"Invalid fields: {string.Join(", ", invalidFields)}");
				}


				foreach (var property in _cachedCompanyProperties)
				{
					if (fieldsSet.Contains(property.Name))
					{
						partialResponse[property.Name.ToLower()] = property.GetValue(fullCompanyResponse);
					}
				}

				return Success<object>(partialResponse);
			}

			return Success<object>(fullCompanyResponse);
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
													comp.WebsiteUrl, comp.Location, comp.PhoneNumber ?? "",
													comp.Email, comp.Fax ?? "");

			var queryable = _companyService.FilterPaginatedQueryable(request.Order);

			var result = await queryable.Select(expression).ToPaginatedAsync(request.PageNumber, request.PageSize);

			return result;
		}

		public async Task<Response<string[]>> Handle(GetPopularCompaniesQuery request, CancellationToken cancellationToken)
		{
			return Success(await _companyService.GetPopularCompanies());

		}

		public async Task<PaginatedResponse<List<GetCompaniesSummaryQueryResponse>>> Handle(GetCompaniesSummaryQuery request, CancellationToken cancellationToken)
		{
			var companies = _companyService.GetPaginatedQueryable();
			return (await _mapper.ProjectTo<GetCompaniesSummaryQueryResponse>(companies).ToPaginatedAsync(request.page, request.size));

		}

		#endregion

	}
}
