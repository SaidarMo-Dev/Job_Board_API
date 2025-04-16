using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class PersonService : IPersonService
	{

		#region Fields
		private readonly IPersonRepository _personRepository;
		#endregion

		#region Constructors
		public PersonService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}
		#endregion

		#region Methods
		public async Task<Person> GetPersonByIdAsync(int Id) => await _personRepository.GetTableAsNoTracking()
			.Where(x => x.PersonId == Id)
			.FirstOrDefaultAsync();

		public async Task<Person> GetPersonByIdWithEncludeAsync(int Id) => await _personRepository.GetTableAsNoTracking()
					.Include(x => x.CountryInfo)
					.Where(x => x.PersonId == Id)
					.FirstOrDefaultAsync();

		public async Task<ICollection<Person>> GetAllAsync()
		{
			return await _personRepository.GetAllAsync();
		}

		public async Task<Person> FindByIdAsync(int id)
		{
			return await _personRepository.FindByIdAsync(id);
		}

		public async Task<Person> AddAsync(Person entity)
		{
			await _personRepository.AddAsync(entity);
			return entity;

		}
		public async Task<Person> UpdateAsync(Person entity)
		{
			await _personRepository.UpdateAsync(entity);

			return entity;
		}


		public async Task<ICollection<Person>> AddRangeAsync(ICollection<Person> entities)
		{
			await _personRepository.AddRangeAsync(entities);
			return entities;
		}

		public async Task<ICollection<Person>> GetAllWithEncludeAsync()
		{
			return await _personRepository.GetTableAsNoTracking().Include(x => x.CountryInfo).ToListAsync();
		}

		public async Task<bool> IsPersonExistAsync(int personId)
		{
			var person = await _personRepository.GetTableAsNoTracking()
								.FirstOrDefaultAsync(x => x.PersonId == personId);

			if (person == null) return false;

			return true;
		}

		public async Task<bool> DeleteAsync(Person person)
		{
			// method 1
			//var person = await  _personRepository.FindByIdAsync(id);

			//if (person == null) return false;
			//await _personRepository.DeleteAsync(person);
			//return true;

			// method 2 
			await _personRepository.DeleteAsync(person);
			return true;

		}

		#endregion


	}

}
