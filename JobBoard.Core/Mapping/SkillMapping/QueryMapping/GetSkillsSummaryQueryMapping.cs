using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile
	{
		public void MapGetSkillsSummaryQuery()
		{
			CreateMap<Skill, GetSkillsSummaryQueryResponse>()
			.ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.SkillId));

		}
	}
}
