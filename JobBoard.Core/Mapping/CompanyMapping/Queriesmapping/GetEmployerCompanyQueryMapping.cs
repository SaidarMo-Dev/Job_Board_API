using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void MapEmployerCompany()
		{
			CreateMap<Company, GetEmployerCompanyQueryResponse>()
				.ForMember(dst => dst.Industries, opt => opt.MapFrom(src => src.CompanyIndustries.Select(ci => ci.Industry)));
		}
	}
}
