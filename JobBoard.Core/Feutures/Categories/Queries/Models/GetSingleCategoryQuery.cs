using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Categories.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Categories.Queries.Models
{
	public class GetSingleCategoryQuery : IRequest<Response<GetSingleCategoryQueryResponse>>
	{
		public int Id { get; set; }
		public GetSingleCategoryQuery(int id)
		{
			Id = id;
		}
	}
}
