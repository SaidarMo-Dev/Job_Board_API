using System.Reflection;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Infrastructure.Extentions.Queries.Companies;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Companies.Queries.Handler
{
	public class CompanyQueryHandler : ResponseHandler,
						IRequestHandler<GetSingleCompanyQuery, Response<object>>,
						IRequestHandler<GetCompaiesQuery, PaginatedResponse<GetListCompaniesQueryesponse>>,
						IRequestHandler<GetPopularCompaniesQuery, Response<string[]>>,
						IRequestHandler<GetCompaniesSummaryQuery, PaginatedResponse<GetCompaniesSummaryQueryResponse>>,
						IRequestHandler<GetCompanyBySlug, Response<GetSingleCompanyQueryResponse>>,
						IRequestHandler<GetCompanyJobs, PaginatedResponse<GlobalJobResponseDto>>,
						IRequestHandler<GetFeaturedCompaniesQuery, PaginatedResponse<GetSingleCompanyQueryResponse>>
	{
		#region Fields
		private readonly ICompanyService _companyService;
		private readonly IMapper _mapper;
		private readonly IJobService _jobService;
		private readonly IFileUrlResolver _fileUrlResolver;
		private static readonly List<PropertyInfo> _cachedCompanyProperties =
	typeof(GetSingleCompanyQueryResponse).GetProperties().ToList();

		#endregion

		#region Constructors
		public CompanyQueryHandler(ICompanyService companyService,
			IMapper mapper,
			IStringLocalizer<SharedResources> stringLocalizer,
			IJobService jobService,
			IFileUrlResolver fileUrlResolver
			)

			: base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
			_jobService = jobService;
			_fileUrlResolver = fileUrlResolver;
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
			var queryable = _companyService.GetCompaniesQueryable();

			queryable = queryable.ApplyCompanySearch(request.Search)
				.ApplyCompanySorting(request.SortBy, request.SortDirection)
				.WhereCompanySizeIs(request.Size)
				.WhereIndustriesIn(request.Industries);

			var result = await _mapper.ProjectTo<GetListCompaniesQueryesponse>(queryable)
				.ToPaginatedAsync(request.Page, request.PageSize);

			if (result.data is null) return result;


			foreach (var company in result.data)
			{
				company.LogoUrl =
					_fileUrlResolver.ResolveCompanyLogo(company.LogoUrl);

			}
			return result;
		}

		public async Task<Response<string[]>> Handle(GetPopularCompaniesQuery request, CancellationToken cancellationToken)
		{
			return Success(await _companyService.GetPopularCompaniesAsync());

		}

		public async Task<PaginatedResponse<GetCompaniesSummaryQueryResponse>> Handle(GetCompaniesSummaryQuery request, CancellationToken cancellationToken)
		{
			var companies = _companyService.GetPaginatedQueryable();
			return (await _mapper.ProjectTo<GetCompaniesSummaryQueryResponse>(companies).ToPaginatedAsync(request.page, request.size));

		}

		public async Task<Response<GetSingleCompanyQueryResponse>> Handle(GetCompanyBySlug request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.slug))
				return BadRequest<GetSingleCompanyQueryResponse>("Invalid slug");

			var company = await _companyService.GetCompanyBySlugAsync(request.slug);

			if (company == null) return NotFound<GetSingleCompanyQueryResponse>("company not found");

			var result = _mapper.Map<GetSingleCompanyQueryResponse>(company);


			result.LogoUrl =
				_fileUrlResolver.ResolveCompanyLogo(company.LogoFile != null ? company.LogoFile.Path : null);

			return Success(result);

		}

		public async Task<PaginatedResponse<GlobalJobResponseDto>> Handle(GetCompanyJobs request, CancellationToken cancellationToken)
		{
			var jobs = _jobService.GetCompanyJobsBySlug(request.Slug);

			var result = await _mapper.ProjectTo<GlobalJobResponseDto>(jobs)
				.ToPaginatedAsync(request.Page, request.PageSize);

			if (result.data is null) return result;

			foreach (var job in result.data)
			{
				job.Company.LogoUrl =
					_fileUrlResolver.ResolveCompanyLogo(job.Company.LogoUrl);
			}



			return result;
		}

		public async Task<PaginatedResponse<GetSingleCompanyQueryResponse>> Handle(GetFeaturedCompaniesQuery request, CancellationToken cancellationToken)
		{
			var query = _companyService.GetFeaturedCompanies();

			var result = await _mapper
				.ProjectTo<GetSingleCompanyQueryResponse>(query)
				.ToPaginatedAsync(request.Page, request.PageSize);

			if (result.data is null) return result;

			foreach (var company in result.data)
			{
				company.LogoUrl =
					_fileUrlResolver.ResolveCompanyLogo(company.LogoUrl);
			}

			return result;
		}

		#endregion

	}
}
