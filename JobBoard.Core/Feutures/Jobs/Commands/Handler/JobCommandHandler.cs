using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Jobs.Commands.Handler
{
	public class JobCommandHandler : ResponseHandler,
				IRequestHandler<AddJobCommand, Response<int>>,
				IRequestHandler<UpdateJobCommand, Response<string>>,
				IRequestHandler<DeleteJobCommand, Response<string>>

	{
		#region Fileds

		private readonly IJobService _jobService;
		private readonly IMapper _mapper;
		private readonly IJobSkillService _jobSkillService;
		private readonly IJobCategoryService _jobCategoryService;
		private readonly ISkillService _skillService;
		private readonly ICategoryService _categoryService;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;
		private readonly ICompanyService _companyService;

		#endregion

		#region Constructors
		public JobCommandHandler(IJobService jobService,
							IMapper mapper,
							IJobSkillService jobSkillService,
							IJobCategoryService jobCategoryService,
							ISkillService skillService,
							ICategoryService categoryService,
							IStringLocalizer<SharedResources> stringLocalizer,
							IAuthorizationService authorizationService,
							ICurrentUserService currentUserService,
							ICompanyService companyService

			) : base(stringLocalizer)
		{
			_jobService = jobService;
			_mapper = mapper;
			_jobSkillService = jobSkillService;
			_jobCategoryService = jobCategoryService;
			_skillService = skillService;
			_categoryService = categoryService;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
			_companyService = companyService;
		}

		#endregion

		#region Handle Functions

		public async Task<Response<int>> Handle(AddJobCommand request, CancellationToken cancellationToken)
		{
			var job = _mapper.Map<JobListing>(request);

			job.DatePosted = DateTime.UtcNow;

			// get created user
			job.CreatedByUserId = _currentUserService.GetCurrentUserId();

			// Get Employer company id
			job.CompanyId = await _companyService.GetCurrentUserCompanyIdAsync();

			// Adding Job skills

			job.JobSkills = request.skillIds?.Select(id => new JobSkill { SkillId = id }).ToList()
							?? new List<JobSkill>();

			// Add job categories

			job.jobCategories = request.CategoryIds?.Select(id => new JobCategory { CategoryId = id }).ToList()
								?? new List<JobCategory>();


			await _jobService.AddNewJobAsync(job);

			return Success(job.JobId);


		}

		public async Task<Response<string>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
		{
			// Fetch the job including the collections (Ensure these are loaded!)
			var job = await _jobService.GetJobByIdWithEncludeSkillsAndCategoriesAsync(request.Id);

			if (job == null) return NotFound<string>($"Job With Id = {request.Id} Not Found");

			// Authorization check
			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				job, AuthorizationPolicies.IsJobCreator);

			if (!isAuthorized.Succeeded) return Forbidden<string>("Access denied");

			// Map basic properties from request to the existing tracked entity
			_mapper.Map(request, job);

			// Update Skills via Collection Manipulation 
			// Remove items that are no longer in the request
			var skillsToRemove = job.JobSkills
				.Where(js => !request.SkillIds.Contains(js.SkillId))
				.ToList();

			foreach (var skill in skillsToRemove)
			{
				job.JobSkills.Remove(skill);
			}
			// Add skills that are in the request but not yet in the entity
			var existingSkillIds = job.JobSkills.Select(js => js.SkillId).ToHashSet();
			foreach (var skillId in request.SkillIds)
			{
				if (!existingSkillIds.Contains(skillId))
				{
					job.JobSkills.Add(new JobSkill { SkillId = skillId });
				}
			}

			// Update Categories via Collection Manipulation
			// Remove items that are no longer in the request
			var categoriesToRemove = job.jobCategories
				.Where(jc => !request.CategoryIds.Contains(jc.CategoryId))
				.ToList();

			foreach (var category in categoriesToRemove)
			{
				job.jobCategories.Remove(category);
			}
			// Add categories that are in the request but not yet in the entity
			var existingCategoryIds = job.jobCategories.Select(jc => jc.CategoryId).ToHashSet();
			foreach (var catId in request.CategoryIds)
			{
				if (!existingCategoryIds.Contains(catId))
				{
					job.jobCategories.Add(new JobCategory { CategoryId = catId });
				}
			}

			// Save once. EF Core handles the inserts and deletes for the join tables automatically.
			await _jobService.UpdateAsync(job);

			return Success<string>();

		}

		public async Task<Response<string>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
		{
			var job = await _jobService.GetJobByIdAsync(request.Id);

			if (job == null) return NotFound<string>($"Job with Id = {request.Id} Not Found");


			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				job, AuthorizationPolicies.IsJobCreator);

			if (!isAuthorized.Succeeded)
				return Forbidden<string>("Access denied");


			var result = await _jobService.DeleteJobAsync(job);

			if (result == false) return BadRequest("Cannot Delete This Job!");

			return Deleted<string>();
		}

		#endregion
	}
}
