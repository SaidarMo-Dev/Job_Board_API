using AutoMapper;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile : Profile
	{
		public CompanyProfile()
		{
			AddSingleCompanyMapping();
			AddMappingForAddCommand();
			AddMappingForUpdateCompany();
			GetListCompaniesQueryMapping();
			MapGetCompaniesSummaryQuery();
			MapGetCompanyBySlug();
		}
	}
}
