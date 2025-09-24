using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void MapGetJobByIdSummaryQuery()
		{
			CreateMap<JobListing, GetJobByIdSummaryQueryResponse>()
				.ForMember(dst => dst.CategoryIds, opt =>
						opt.MapFrom(src => src.jobCategories.Select(jc => jc.CategoryId)))
			.ForMember(dst => dst.SkillIds, opt =>
						opt.MapFrom(src => src.JobSkills.Select(jc => jc.SkillId)))
			.ForMember(dst => dst.CretaedByUser, opt =>
						opt.MapFrom(src => src.CreatedByUser.FullName));

		}
	}
}
