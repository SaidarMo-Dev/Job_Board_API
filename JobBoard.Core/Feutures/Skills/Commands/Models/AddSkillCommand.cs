using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Commands.Models
{
	public class AddSkillCommand : IRequest<Response<int>>
	{
		public required string Name { get; set; }
		public string? Description { get; set; }

	}
}
