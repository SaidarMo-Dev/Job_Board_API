using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class SetApplicationStatusToAcceptedCommand : IRequest<Response<string>>
	{
		public int ApplicationId { get; set; }
	}
}
