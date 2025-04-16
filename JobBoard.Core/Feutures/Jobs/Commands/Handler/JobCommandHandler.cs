using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Handler
{
	public class JobCommandHandler : ResponseHandler, IRequestHandler<AddJobCommand, Response<int>>
	{
		private readonly IJobService _jobService;
		private readonly IMapper _mapper;
		private readonly IJobSkillService _jobSkillService;
		private readonly IJobCategoryService _jobCategoryService;
		#region Fileds

		#endregion

		#region Constructors
		public JobCommandHandler(IJobService jobService,
							IMapper mapper,
							IJobSkillService jobSkillService,
							IJobCategoryService jobCategoryService)
		{
			_jobService = jobService;
			_mapper = mapper;
			_jobSkillService = jobSkillService;
			_jobCategoryService = jobCategoryService;
		}

		#endregion

		#region Handle Functions

		public async Task<Response<int>> Handle(AddJobCommand request, CancellationToken cancellationToken)
		{
			var job = _mapper.Map<JobListing>(request);

			var result = await _jobService.AddNewJobAsync(job);


			// we add Job skills
			if (job.JobId != 0)
			{

				var jobSkills = new List<JobSkill>();
				request.skillsId.ForEach(Id => jobSkills.Add(new JobSkill { JobListingId = job.JobId, SkillId = Id }));

				await _jobSkillService.AddRangeAsync(jobSkills);

			}

			// add categories
			var Jobcategories = new List<JobCategory>();

			request.CategoriesId.ForEach(Id => Jobcategories.Add(new JobCategory { JobListingId = job.JobId, CategoryId = Id }));

			await _jobCategoryService.AddRangeAsync(Jobcategories);

			return Success(job.JobId);
		}

		#endregion
	}
}
