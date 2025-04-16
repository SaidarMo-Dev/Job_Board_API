using JobBoard.Core.Feutures.Skills.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.SkillMapping
{
	public partial class SkillProfile
	{
		public void addMappingForUpdateSkillCommand()
		{
			CreateMap<UpdateSkillCommand, Skill>();
		}
	}
}
