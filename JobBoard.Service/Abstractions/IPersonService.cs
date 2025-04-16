using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface IPersonService
	{
		Task<Person> GetPersonByIdAsync(int Id);
		Task<Person> GetPersonByIdWithEncludeAsync(int Id);
		Task<ICollection<Person>> GetAllAsync();
		Task<ICollection<Person>> GetAllWithEncludeAsync();
		Task<Person> FindByIdAsync(int id);
		Task<Person> AddAsync(Person entity);
		Task<ICollection<Person>> AddRangeAsync(ICollection<Person> entities);
		Task<Person> UpdateAsync(Person entity);
		Task<bool> DeleteAsync(Person person);
		Task<bool> IsPersonExistAsync(int personId);
	}

}
