using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
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

			var result = await _jobService.AddNewJobAsync(job);


			// adding Job skills and job categories
			if (job.JobId != 0)
			{
				var jobSkills = new HashSet<JobSkill>();
				var Jobcategories = new HashSet<JobCategory>();

				foreach (int id in request.skillsId)
				{
					var Exist = _skillService.IsExistById(id);
					if (Exist)
						jobSkills.Add(new JobSkill { JobListingId = job.JobId, SkillId = id });
				}
				await _jobSkillService.AddRangeAsync(jobSkills);

				foreach (int Id in request.skillsId)
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

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), Oldjob, new JobCreatorRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<string>();

			if (Oldjob == null) return NotFound<string>($"Job With Id = {request.Id} Not Found");

			var newJob = _mapper.Map(request, Oldjob);

			await _jobService.UpdateAsync(newJob);

			// update job Skills and categories


			// update job skills
			foreach (var Id in request.skillIds)
			{
				bool Exist = await _jobSkillService.IsExistById(request.Id, Id);

				if (!Exist && _skillService.IsExistById(Id))
				{
					await _jobSkillService.AddAsync(new JobSkill { JobListingId = newJob.JobId, SkillId = Id });
				}
			}

			foreach (var item in Oldjob.Jobkills)
			{
				if (!request.skillIds.Contains(item.SkillId))
				{
					await _jobSkillService.DeleteAsync(item);
				}
			}


			// update JobCategories
			foreach (var Id in request.CategorieIds)
			{
				bool Exist = await _jobCategoryService.IsExistById(request.Id, Id);

				if (!Exist && _categoryService.IsExistById(Id))
				{
					await _jobCategoryService.AddAsync(new JobCategory { JobListingId = newJob.JobId, CategoryId = Id });
				}
			}

			foreach (var item in Oldjob.jobCategories)
			{
				if (!request.skillIds.Contains(item.CategoryId))
				{
					await _jobCategoryService.DeleteAsync(item);
				}
			}

			return Success<string>();

		}

		public async Task<Response<string>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
		{
			var job = await _jobService.GetJobByIdAsync(request.Id);

			var userRoles = await _currentUserService.GetCurrentUserRoles();

			// of not Admin then apply resource based Authorizatin for job creator
			if (!userRoles.Contains("Admin"))
			{
				var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), job, new JobCreatorRequirement());
				if (!isAuthorized.Succeeded) return Forbidden<string>();

			}

			if (job == null) return NotFound<string>($"Job with Id = {request.Id} Not Found");


			var result = await _jobService.DeleteJobAsync(job);

			if (result == false) return BadRequest("Cannot Delete This Job!");

			return Deleted<string>();
		}

		#endregion
	}
}
