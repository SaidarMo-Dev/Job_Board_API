using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class SetApplicationStatusToRemovedCommand : IRequest<Response<string>>
	{
		public int ApplicationId { get; set; }
	}
}
