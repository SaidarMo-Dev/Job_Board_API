using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void GetJobSkillsMapping()
		{
			CreateMap<Skill, GetJobSkillsQueryResponse>()
				.ForMember(dst => dst.SkillId, opt => opt.MapFrom(src => src.SkillId))
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description));


		}
	}
}
