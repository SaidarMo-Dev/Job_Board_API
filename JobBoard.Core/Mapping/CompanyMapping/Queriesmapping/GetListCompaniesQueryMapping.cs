using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void GetListCompaniesQueryMapping()
		{

			CreateMap<Company, GetListCompaniesQueryesponse>()

					.ForMember(dst => dst.LogoUrl,
						opt => opt.MapFrom(src => src.LogoFile != null ? src.LogoFile.Path : null));

		}
	}
}
