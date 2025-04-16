using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Commands.Models
{
	public class DeleteSkillCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
