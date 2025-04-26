using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Commands.Models
{
	public class UpdateApplicationCommand : IRequest<Response<string>>
	{
		public int ApplicationId { get; set; }
		public string? Description { get; set; }
		public ApplicationStatusEnum Status { get; set; }

	}
}
