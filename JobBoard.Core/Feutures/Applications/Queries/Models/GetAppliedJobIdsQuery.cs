using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Models
{
	public class GetAppliedJobIdsQuery : IRequest<Response<int[]>>
	{
	}
}
