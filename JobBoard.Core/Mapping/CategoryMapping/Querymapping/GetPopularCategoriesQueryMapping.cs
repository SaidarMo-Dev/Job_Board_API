using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CategoryMapping
{
	public partial class CategoryProfile
	{
		public void GetPopularCategoriesQueryMapping()
		{
			CreateMap<Category, GetPopularCategoriesQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.CategoryId));
		}
	}
}
