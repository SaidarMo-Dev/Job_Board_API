using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Responses;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void MapGetJobApplicantsSummary()
		{
			CreateMap<JobApplicantsSummaryResponse, GetJobApplicantSummaryResponse>();
		}
	}
}
