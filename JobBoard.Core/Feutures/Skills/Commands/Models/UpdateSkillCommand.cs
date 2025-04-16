using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Commands.Models
{
	public class UpdateSkillCommand : IRequest<Response<string>>
	{
		public int SkillId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

	}
}
