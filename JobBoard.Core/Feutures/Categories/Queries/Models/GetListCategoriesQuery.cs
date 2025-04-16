using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Categories.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Queries.Models
{
	public class GetListCategoriesQuery : IRequest<Response<List<GetListCategoriesQueryResponse>>>
	{
	}
}
