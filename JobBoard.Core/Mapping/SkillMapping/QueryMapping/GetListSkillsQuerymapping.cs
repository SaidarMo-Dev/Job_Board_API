using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile
	{
		public void AddListSkillsMapping()
		{
			CreateMap<Skill, GetListSkillsQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.SkillId))
				.ForMember(x => x.CreateDate, opt => opt.MapFrom(
								src => src.CreateDate.ToString()));

		}
	}
}
