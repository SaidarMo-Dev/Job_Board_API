using JobBoard.Core.Feutures.Employer.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.EmployerMapping
{
	public partial class EmployerProfile
	{
		public void MapGetEmployerPostedJobs()
		{
			CreateMap<JobListing, GetEmployerPostedJobsQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.JobId))
				.ForMember(x => x.Company, opt => opt.MapFrom(src => src.Company.CompanyName))
				.ForMember(x => x.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)))
				.ForMember(x => x.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
				.ForMember(x => x.ApplicantsCount, opt => opt.MapFrom(src => src.applications.Count()))
				.ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
				.ForMember(x => x.PostedDate, opt => opt.MapFrom(src => src.DatePosted))
				.ForMember(x => x.ExpiryDate, opt => opt.MapFrom(src => src.DateExpired))
				;
		}
	}
}
