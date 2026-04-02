using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void GetListCompaniesQueryMapping()
		{
			CreateMap<Company, GetListCompaniesQueryesponse>()

				.ForMember(dst => dst.TotalJobs, opt =>
								opt.MapFrom(src => src.JobListings != null ? src.JobListings.Count() : 0))
				.ForMember(dst => dst.TotalOpenJobs, opt =>
					opt.MapFrom(src => src.JobListings != null ?
								src.JobListings.Count(j => j.Status == Data.enums.JobStatusEnum.Active &&
								j.DateExpired > DateTime.UtcNow) : 0))

				.ForMember(dst => dst.CreatedByUser,
					opt => opt.MapFrom(src => src.CreatedByUser.FullName))

				.ForMember(dst => dst.LogoUrl,
					opt => opt.MapFrom(src => src.LogoFile != null ? src.LogoFile.Path : null))

				.ForMember(dst => dst.Industries,
					opt => opt.MapFrom(src => src.CompanyIndustries
						.Select(ci => ci.Industry.Name)
						.Distinct()));
		}
	}
}
