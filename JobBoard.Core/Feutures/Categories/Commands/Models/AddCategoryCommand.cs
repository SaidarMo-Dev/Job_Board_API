using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Commands.Models
{
	public class AddCategoryCommand : IRequest<Response<int>>
	{
		public required string Name { get; set; }
		public string? Description { get; set; }
	}
}
