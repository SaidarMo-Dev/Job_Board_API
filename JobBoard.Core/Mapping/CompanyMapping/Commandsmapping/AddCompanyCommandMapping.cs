using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void AddMappingForAddCommand()
		{
			CreateMap<AddCompanyCommand, Company>();
		}
	}
}
