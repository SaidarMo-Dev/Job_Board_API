using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Commands.Models
{
	public class DeleteCompanyCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteCompanyCommand(int id)
		{
			Id = id;
		}
	}
}
