using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void AddSingleCompanyMapping()
		{
			CreateMap<Company, GetSingleCompanyQueryResponse>()
				.ForMember(dst => dst.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
				.ForMember(dst => dst.TotalJobs, opt => opt.MapFrom(src => src.JobListings != null ? src.JobListings.Count() : 0))
				.ForMember(dst => dst.TotalOpenJobs, opt =>
					opt.MapFrom(src => src.JobListings != null ?
						src.JobListings.Count(j => j.Status == Data.enums.JobStatusEnum.Active
								&& j.DateExpired > DateTime.UtcNow) : 0));
		}
	}
}
