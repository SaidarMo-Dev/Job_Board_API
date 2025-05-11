using JobBoard.Core.Feutures.Categories.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CategoryMapping
{
	public partial class CategoryProfile
	{
		public void AddCategoryMapping()
		{
			CreateMap<AddCategoryCommand, Category>();

		}
	}
}
