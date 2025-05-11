using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Commands.Models
{
	public class DeleteCategoryCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
