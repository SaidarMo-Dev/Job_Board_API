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
				.ForMember(dst => dst.CreatedByUser, opt =>
								opt.MapFrom(src => src.CreatedByUser.FullName));

		}
	}
}
