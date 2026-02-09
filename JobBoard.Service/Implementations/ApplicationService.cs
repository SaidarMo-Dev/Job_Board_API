using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Data.Responses;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class ApplicationService : IApplicationService
	{
		#region Fields
		private readonly IApplicationRepository _applicationRepository;

		#endregion

		#region Constructors
		public ApplicationService(IApplicationRepository applicationRepository)
		{
			_applicationRepository = applicationRepository;
		}



		#endregion

		#region Methods
		public async Task<bool> AddAsync(Application application)
		{
			try
			{
				await _applicationRepository.AddAsync(application);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> DeleteAsync(Application app)
		{
			await _applicationRepository.DeleteAsync(app);
			return true;
		}

		public async Task<Application> GetByIdAsync(int Id)
		{
			var application = await _applicationRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(app => app.ApplicationId.Equals(Id));

			return application;
		}

		public async Task<Application> GetByIdWithIncludeAsync(int Id)
		{
			var application = await _applicationRepository.GetTableAsNoTracking()
						.Include(x => x.UserInfo)
						.Include(x => x.JobListing).ThenInclude(x => x.Company)
						.FirstOrDefaultAsync(app => app.ApplicationId.Equals(Id));

			return application;
		}

		public async Task<bool> HasActiveOrAcceptedApplicationWithJobAsync(int UserId, int jobId)
		{
			var result = await _applicationRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(x => x.UserId == UserId
							&& x.JobId == jobId &&
							(x.Status == ApplicationStatusEnum.Accepted ||
							x.Status == ApplicationStatusEnum.Pending));


			return result != null;
		}

		public async Task<bool> UpdateAsync(Application application)
		{
			await _applicationRepository.UpdateAsync(application);
			return true;
		}

		public async Task<List<Application>> GetApplicationsByJobIdAsync(int JobId)
		{
			var result = _applicationRepository.GetTableAsNoTracking()
						.Where(x => x.JobId.Equals(JobId))
						.Include(x => x.JobListing)
						.Include(x => x.UserInfo).ThenInclude(x => x.Country);

			return await result.ToListAsync();
		}

		public IQueryable<Application> GetUserApplicationsQueryable(int UserId)
		{
			var result = _applicationRepository.GetTableAsNoTracking()
							.Where(x => x.UserId.Equals(UserId)).AsQueryable();

			return result;
		}

		public async Task<IReadOnlyList<RecentApplicationsResponse>> GetRecentApplicationsAsync(int userId, int take)
		{
			if (take <= 0) take = 3;

			return (await _applicationRepository.GetTableAsNoTracking()
						.Where(ap => ap.UserId == userId)
						.OrderByDescending(ap => ap.CreatedOn)
						.Take(take)
						.Select(app => new RecentApplicationsResponse
						{
							Id = app.ApplicationId,
							Position = app.JobListing.Title,
							Company = app.JobListing.Company.CompanyName,
							ApplicantDate = app.CreatedOn,
							Status = app.Status
						}).ToListAsync());

		}

		public async Task<int[]> GetAppliedJobIds(int userId)
		{
			return (await _applicationRepository.GetTableAsNoTracking()
								.Where(app => app.UserId == userId)
								.Select(x => x.JobListing.JobId).ToArrayAsync());
		}

		public async Task AttachResumeAsync(int applicationId, int resumeFileId)
		{
			var application = await _applicationRepository.GetTableAsNoTracking()
				.FirstOrDefaultAsync(app => app.ApplicationId == applicationId);

			if (application == null)
				throw new Exception("Application Not Found for Id:" + applicationId);


			// Idempotency
			if (application.ResumeFileId == resumeFileId)
				return;

			if (application.ResumeFileId != null)
				throw new Exception("Resume already attached for appliction with id :" + applicationId);


			application.ResumeFileId = resumeFileId;
			await _applicationRepository.UpdateAsync(application);

		}

		public async Task DeleteByIdAsync(int applicationId)
		{
			var app = await _applicationRepository.GetTableAsNoTracking().
				FirstOrDefaultAsync(ap => ap.ApplicationId == applicationId);

			if (app != null)
				await _applicationRepository.DeleteAsync(app);

		}


		#endregion

	}
}