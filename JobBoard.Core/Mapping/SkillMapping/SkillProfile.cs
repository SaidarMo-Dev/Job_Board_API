using AutoMapper;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile : Profile
	{
		public SkillProfile()
		{
			MapGetSingleSkillQuery();
			MapGetListSkills();
			MapAddSkillCommand();
			MapUpdateSkillCommand();
			MapGetSkillsSummaryQuery();
		}
	}
}
