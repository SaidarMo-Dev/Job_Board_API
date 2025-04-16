using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void GetJobCategoriesQueryMapping()
		{
			CreateMap<Category, GetJobCategoriesQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.CategoryId));

		}
	}
}
