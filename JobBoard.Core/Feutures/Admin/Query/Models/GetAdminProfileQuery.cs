using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Admin.Query.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Admin.Query.Models
{
	public class GetAdminProfileQuery : IRequest<Response<GetAdminProfileQueryResponse>>
	{

	}
}
