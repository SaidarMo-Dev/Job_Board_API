using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile
	{
		public void AddListSkillsMapping()
		{
			CreateMap<Skill, GetListSkillsQueryResponse>();

		}
	}
}
