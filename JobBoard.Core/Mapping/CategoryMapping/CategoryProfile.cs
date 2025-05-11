using AutoMapper;

namespace JobBoard.Core.Mapping.CategoryMapping
{
	public partial class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			AddSingleCategorymapping();
			AddGetListMapping();
			AddCategoryMapping();
			UpdateCategoryMapping();
		}
	}
}
