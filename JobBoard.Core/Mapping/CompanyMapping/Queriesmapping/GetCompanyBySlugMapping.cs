using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void MapGetCompanyBySlug()
		{
			CreateMap<Company, GetCompanyBySlugQueryResponse>();
		}
	}
}
