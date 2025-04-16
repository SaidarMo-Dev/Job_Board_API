using JobBoard.Core.Feutures.Companies.Queries.Results;
using JobBoard.Core.Wrapers;
using JobBoard.Data.Helpers.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetPaginatedListCompanyQuery : IRequest<PaginatedResponse<List<GetPaginatedListCompaniesQueryResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public OrderCompanyEnum Order { get; set; }
		//public GetPaginatedListCompanyQuery(int pageNumber = 1, int pageSize = 10, OrderCompanyEnum order = OrderCompanyEnum.OrderByID)
		//{
		//	PageNumber = pageNumber;
		//	PageSize = pageSize;
		//	Order = order;
		//}
	}
}
