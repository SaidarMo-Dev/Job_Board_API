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
using Microsoft.EntityFrameworkCore;
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
		private readonly IFileResourceService _fileResourceService;
		private readonly ICompanyFileStitcher _stitcher;
		private static readonly List<PropertyInfo> _cachedCompanyProperties =
	typeof(GetSingleCompanyQueryResponse).GetProperties().ToList();

		#endregion

		#region Constructors
		public CompanyQueryHandler(ICompanyService companyService,
			IMapper mapper,
			IStringLocalizer<SharedResources> stringLocalizer,
			IJobService jobService,
			IFileUrlResolver fileUrlResolver,
			IFileResourceService fileResourceService,
			ICompanyFileStitcher stitcher
			)

			: base(stringLocalizer)
		{
			_companyService = companyService;
			_mapper = mapper;
			_jobService = jobService;
			_fileUrlResolver = fileUrlResolver;
			_fileResourceService = fileResourceService;
			_stitcher = stitcher;
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
			// Get the base query for companies with split query to avoid cartesian explosion
			var queryable = _companyService.GetCompaniesQueryable().AsSplitQuery();

			// Apply filters and sorting based on request parameters
			queryable = queryable
				.ApplyCompanySearch(request.Search)
				.ApplyCompanySorting(request.SortBy, request.SortDirection)
				.WhereCompanySizeIs(request.Size)
				.WhereIndustriesIn(request.Industries);

			// Project the filtered companies to DTOs and apply pagination
			// This executes the query for the current page only
			var result = await _mapper
				.ProjectTo<GetListCompaniesQueryesponse>(queryable)
				.ToPaginatedAsync(request.Page, request.PageSize);

			// If no companies found, return early
			if (result.data is null) return result;

			// Extract company IDs from the paginated results
			// This allows us to fetch job counts only for visible companies
			var companyIds = result.data
				.Select(c => c.CompanyId)
				.ToList();

			// Current UTC time for calculating open jobs
			var now = DateTime.UtcNow;

			// Fetch job statistics for all companies in a single query
			// Group by company to get total jobs and open jobs
			var jobStats = await _jobService.GetJobsQueryable()
				.Where(j => companyIds.Contains(j.CompanyId))      // Only consider companies on the current page
				.GroupBy(j => j.CompanyId)
				.Select(g => new
				{
					CompanyId = g.Key,
					TotalJobs = g.Count(),                          // Total jobs per company
					OpenJobs = g.Count(j =>
						j.Status == Data.enums.JobStatusEnum.Active && // Only active jobs
						j.DateExpired > now)                            // Only jobs that haven't expired
				})
				.ToDictionaryAsync(x => x.CompanyId);              // Convert to dictionary for fast lookup


			// handle only banner and logo for companies using stitcher service

			await _stitcher.AttachLogosAndBannersAsync(
				result.data,
				c => c.CompanyId,           // Drill down to the company ID
				(c, url) => c.LogoUrl = url, // Drill down to set the URL
				(c, url) => c.BannerUrl = url,
				cancellationToken);


			// Merge job stats and resolve company logos
			foreach (var company in result.data)
			{
				if (jobStats.TryGetValue(company.CompanyId, out var stats))
				{
					company.TotalJobs = stats.TotalJobs;          // Assign total jobs
					company.TotalOpenJobs = stats.OpenJobs;       // Assign open jobs
				}
				else
				{
					company.TotalJobs = 0;                        // No jobs found
					company.TotalOpenJobs = 0;
				}

			}

			// Return the final paginated list with stats and resolved logos
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

			// handle company logo and banner

			await _stitcher.AttachLogoAndBannerAsync
				(result,
				result.CompanyId,
				(c, url) => c.LogoUrl = url,
				(c, url) => c.BannerUrl = url);

			return Success(result);

		}

		public async Task<PaginatedResponse<GlobalJobResponseDto>> Handle(GetCompanyJobs request, CancellationToken cancellationToken)
		{
			var jobs = _jobService.GetCompanyJobsBySlug(request.Slug);

			var result = await _mapper.ProjectTo<GlobalJobResponseDto>(jobs)
				.ToPaginatedAsync(request.Page, request.PageSize);

			if (result.data is null) return result;



			await _stitcher.AttachLogosAsync(
				result.data,
				j => j.Company.CompanyId,           // Drill down to the company ID
				(j, url) => j.Company.LogoUrl = url, // Drill down to set the URL
				cancellationToken);

			return result;
		}

		public async Task<PaginatedResponse<GetSingleCompanyQueryResponse>> Handle(GetFeaturedCompaniesQuery request, CancellationToken cancellationToken)
		{
			var query = _companyService.GetFeaturedCompanies();

			var result = await _mapper
				.ProjectTo<GetSingleCompanyQueryResponse>(query)
				.ToPaginatedAsync(request.Page, request.PageSize);

			if (result.data is null) return result;


			await _stitcher.AttachLogosAsync(
				result.data,
				j => j.CompanyId,           // Drill down to the company ID
				(j, url) => j.LogoUrl = url, // Drill down to set the URL
				cancellationToken);

			return result;
		}

		#endregion

	}
}
