using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Models
{
	public record GetCurrentUserProfileQuery : IRequest<Response<GetCurrentUserProfileResponse>>;
}
