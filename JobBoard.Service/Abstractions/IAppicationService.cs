using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IApplicationService
	{
		Task<Application> GetByIdAsync(int Id);
		Task<Application> GetByIdWithIncludeAsync(int Id);
		Task<bool> AddAsync(Application application);
		Task<bool> UpdateAsnyc(Application application);
		Task<bool> HasActiveOrAcceptedApplicationAsnyc(int UserId);
		Task<bool> DeleteAsync(Application app);

	}
}
