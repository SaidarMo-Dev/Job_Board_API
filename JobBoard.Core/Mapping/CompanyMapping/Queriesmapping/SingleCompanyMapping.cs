using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void AddSingleCompanyMapping()
		{
			CreateMap<Company, GetSingleCompanyQueryResponse>()
				.ForMember(dst => dst.LogoUrl, opt => opt.MapFrom(src => src.LogoFile != null ? src.LogoFile.Path : null))
				.ForMember(dst => dst.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.FullName));
		}
	}
}
