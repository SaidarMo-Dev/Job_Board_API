using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Commands.Models
{
	public class UpdateCategoryCommand : IRequest<Response<string>>
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
