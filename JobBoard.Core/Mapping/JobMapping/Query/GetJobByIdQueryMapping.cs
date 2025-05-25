using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void GetJobByIdQueryMapping()
		{
			CreateMap<JobListing, GetJobByIdQueryResponse>()
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.company.CompanyName))
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(x => x.DatePosted, opt => opt.MapFrom(src => src.DatePosted.ToShortDateString()))
				.ForMember(x => x.status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(x => x.CretaedByUser, opt => opt.MapFrom(src => src.UserInfo.FullName));

		}
	}
}
