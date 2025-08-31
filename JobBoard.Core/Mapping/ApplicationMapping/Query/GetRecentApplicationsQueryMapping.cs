using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Data.Responses;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void GetRecentApplicationsQueryMapping()
		{
			CreateMap<RecentApplicationsResponse, GetRecentApplicationsQueryResponse>()
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()));
		}
	}
}
