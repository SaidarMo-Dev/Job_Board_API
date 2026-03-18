using System.Reflection;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Companies.Queries.Handler
{
	public class CompanyQueryHandler : ResponseHandler,
						IRequestHandler<GetSingleCompanyQuery, Response<object>>,
						IRequestHandler<GetCompaiesQuery, PaginatedResponse<GetListCompaniesQueryesponse>>,
						IRequestHandler<GetPopularCompaniesQuery, Response<string[]>>,
						IRequestHandler<GetCompaniesSummaryQuery, PaginatedResponse<GetCompaniesSummaryQueryResponse>>
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

		public async Task<PaginatedResponse<GetListCompaniesQueryesponse>> Handle(GetCompaiesQuery request, CancellationToken cancellationToken)
		{
			var queryable = _companyService.GetCompaniesQueryable(request.Name, request.Sort);

			return (await _mapper.ProjectTo<GetListCompaniesQueryesponse>(queryable).ToPaginatedAsync(request.Page, request.PageSize));
		}

		public async Task<Response<string[]>> Handle(GetPopularCompaniesQuery request, CancellationToken cancellationToken)
		{
			return Success(await _companyService.GetPopularCompanies());

		}

		public async Task<PaginatedResponse<GetCompaniesSummaryQueryResponse>> Handle(GetCompaniesSummaryQuery request, CancellationToken cancellationToken)
		{
			var companies = _companyService.GetPaginatedQueryable();
			return (await _mapper.ProjectTo<GetCompaniesSummaryQueryResponse>(companies).ToPaginatedAsync(request.page, request.size));

		}

		#endregion

	}
}
