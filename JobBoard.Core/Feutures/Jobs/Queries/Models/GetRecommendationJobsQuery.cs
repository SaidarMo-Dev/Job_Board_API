using JobBoard.Core.Bases;
using JobBoard.Core.Common.DTOs;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetRecommendationJobsQuery : IRequest<Response<List<JobResponseDto>>>
	{
	}
}
