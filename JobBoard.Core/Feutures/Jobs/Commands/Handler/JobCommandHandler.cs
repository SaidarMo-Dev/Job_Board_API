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
							ICurrentUserService currentUserService

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
		}

		#endregion

		#region Handle Functions

		public async Task<Response<int>> Handle(AddJobCommand request, CancellationToken cancellationToken)
		{
			var job = _mapper.Map<JobListing>(request);

			job.DatePosted = DateTime.UtcNow;

			// get created user
			job.CreatedByUserId = _currentUserService.GetCurrentUserId();

			// if admin job approved directly else job waitin to approved

			var userRoles = await _currentUserService.GetCurrentUserRoles();

			if (userRoles.Contains("Admin"))
				job.Status = Data.enums.JobStatusEnum.Active;
			else
				job.Status = Data.enums.JobStatusEnum.Pending;

			// add job 
			var result = await _jobService.AddNewJobAsync(job);


			// adding Job skills and job categories
			if (job.JobId != 0)
			{
				var jobSkills = new HashSet<JobSkill>();
				var Jobcategories = new HashSet<JobCategory>();

				foreach (int id in request.skillIds)
				{
					var Exist = _skillService.IsExistById(id);
					if (Exist)
						jobSkills.Add(new JobSkill { JobListingId = job.JobId, SkillId = id });
				}
				await _jobSkillService.AddRangeAsync(jobSkills);

				foreach (int Id in request.skillIds)
				{
					var Exist = _categoryService.IsExistById(Id);
					if (Exist)
						Jobcategories.Add(new JobCategory { JobListingId = job.JobId, CategoryId = Id });
				}

				await _jobCategoryService.AddRangeAsync(Jobcategories);

			}

			return Success(job.JobId);


		}

		public async Task<Response<string>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
		{
			var Oldjob = await _jobService.GetJobByIdWithEncludeSkillsAndCategoriesAsync(request.Id);

			if (Oldjob == null) return NotFound<string>($"Job With Id = {request.Id} Not Found");

			var isAuthorized = await _authorizationService.AuthorizeAsync(
			_currentUserService.GetCurrentUserPrincipal(),
			Oldjob, AuthorizationPolicies.IsJobCreator);

			if (!isAuthorized.Succeeded)
				return Forbidden<string>("Access denied");

			var newJob = _mapper.Map(request, Oldjob);

			if (!newJob.DateExpired.HasValue)
				newJob.DateExpired = Oldjob.DateExpired;

			await _jobService.UpdateAsync(newJob);

			// update job Skills and categories


			// update job skills
			foreach (var Id in request.SkillIds)
			{
				bool Exist = await _jobSkillService.IsExistById(request.Id, Id);

				if (!Exist && _skillService.IsExistById(Id))
				{
					await _jobSkillService.AddAsync(new JobSkill { JobListingId = newJob.JobId, SkillId = Id });
				}
			}

			foreach (var item in Oldjob.JobSkills)
			{
				if (!request.SkillIds.Contains(item.SkillId))
				{
					await _jobSkillService.DeleteAsync(item);
				}
			}


			// update JobCategories
			foreach (var Id in request.CategoryIds)
			{
				bool Exist = await _jobCategoryService.IsExistById(request.Id, Id);

				if (!Exist && _categoryService.IsExistById(Id))
				{
					await _jobCategoryService.AddAsync(new JobCategory { JobListingId = newJob.JobId, CategoryId = Id });
				}
			}

			foreach (var item in Oldjob.jobCategories)
			{
				if (!request.SkillIds.Contains(item.CategoryId))
				{
					await _jobCategoryService.DeleteAsync(item);
				}
			}

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
