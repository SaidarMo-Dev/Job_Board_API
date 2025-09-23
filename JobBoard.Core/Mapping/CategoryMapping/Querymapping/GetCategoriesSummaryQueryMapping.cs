using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CategoryMapping
{
	public partial class CategoryProfile
	{
		public void MapGetCategoriesSummaryQuery()
		{
			CreateMap<Category, GetCategoriesSummaryQueryResponse>()
					.ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.CategoryId));
		}
	}
}
