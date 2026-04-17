using JobBoard.Core.Common.Helpers;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void MapEmployerCompany()
		{
			CreateMap<Company, GetEmployerCompanyQueryResponse>()
				.ForMember(dst => dst.CompanySize, opt => opt.MapFrom(src => CompanySizeHelper.GetCompanySizeFromString(src.CompanySize).ToString()))
				.ForMember(dst => dst.Industries, opt => opt.MapFrom(src => src.CompanyIndustries.Select(ci => ci.Industry)));
		}
	}
}
