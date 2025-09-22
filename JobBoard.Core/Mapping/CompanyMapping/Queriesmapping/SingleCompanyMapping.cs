using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void AddSingleCompanyMapping()
		{
			CreateMap<Company, GetSingleCompanyQueryResponse>()
				.ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.CompanyId))
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CompanyName));
		}
	}
}
