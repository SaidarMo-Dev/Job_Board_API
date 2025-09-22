using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetSingleCompanyQuery(int id, string? fields) : IRequest<Response<object>>
	{
		public int Id { get; set; } = id;
		public string? Fields { get; set; } = fields;
	}
}
