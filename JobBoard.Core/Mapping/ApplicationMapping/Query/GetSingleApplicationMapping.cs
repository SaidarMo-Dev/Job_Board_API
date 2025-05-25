using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Data.Entities;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void GetSingleApplicationMapping()
		{
			CreateMap<Application, GetSingleApplictionQueryResponse>()
				.ForMember(x => x.UserResponse, opt => opt.MapFrom(src => src.UserInfo))
				.ForMember(x => x.JobResponse, opt => opt.MapFrom(src => src.JobListing));


			CreateMap<User, UserResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(x => x.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

			CreateMap<JobListing, ApplicationJobResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.JobId))
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.company.CompanyName));
		}
	}
}
