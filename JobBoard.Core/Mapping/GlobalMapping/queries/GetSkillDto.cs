using JobBoard.Core.Common.DTOs;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile
	{
		public void GetSkillDtoMapping()
		{

			CreateMap<Skill, SkillDto>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.SkillId));

		}
	}
}
