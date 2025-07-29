using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using JobBoard.Data.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Admin.Query.Models
{
	public class GetUsersQuery : IRequest<PaginatedResponse<List<UserManagementResponse>>>
	{
		public int Page { get; set; }
		public int Size { get; set; }
		public string? Search { get; set; }
		public FilterByRole? FilterByRole { get; set; }
		public FilterByStatus? FilterStatus { get; set; }
	}
}
