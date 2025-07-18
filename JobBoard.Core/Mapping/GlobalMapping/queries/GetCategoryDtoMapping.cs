using JobBoard.Core.Common.DTOs;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile
	{
		public void GetCategoryDtoMapping()
		{

			CreateMap<Category, CategoryDto>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.CategoryId))
				.ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name));
		}

	}
}
