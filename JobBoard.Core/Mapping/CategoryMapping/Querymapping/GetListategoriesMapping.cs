using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CategoryMapping
{
	public partial class CategoryProfile
	{
		public void AddGetListMapping()
		{
			CreateMap<Category, GetListCategoriesQueryResponse>();

		}
	}
}
