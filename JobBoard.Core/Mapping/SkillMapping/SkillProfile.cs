using AutoMapper;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile : Profile
	{
		public SkillProfile()
		{
			AddSingleSkillQueryMapping();
			AddListSkillsMapping();

			AddMappingForAddSkillCommand();
			addMappingForUpdateSkillCommand();
		}
	}
}
