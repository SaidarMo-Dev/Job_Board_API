using JobBoard.Data.Entities;
using JobBoard.Data.enums;
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
						.Include(x => x.userInfo)
						.Include(x => x.jobListing).ThenInclude(x => x.company)
						.FirstOrDefaultAsync(app => app.ApplicationId.Equals(Id));

			return application;
		}

		public async Task<bool> HasActiveOrAcceptedApplicationAsnyc(int UserId)
		{
			var result = await _applicationRepository.GetTableAsNoTracking()
						.FirstOrDefaultAsync(x => x.UserId == UserId &&
											(x.status == ApplicationStatusEnum.Accepted ||
											x.status == ApplicationStatusEnum.Pending)
											);

			return result != null;
		}

		public async Task<bool> UpdateAsnyc(Application application)
		{
			await _applicationRepository.UpdateAsync(application);
			return true;
		}


		#endregion

	}
}