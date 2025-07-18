using AutoMapper;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile : Profile
	{
		public GlobaleMappingProfile()
		{
			GetSkillDtoMapping();
			GetJobSummaryDto();
			GetCategoryDtoMapping();

		}
	}
}
