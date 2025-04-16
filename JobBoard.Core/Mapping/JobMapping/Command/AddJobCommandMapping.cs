using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Data.Entities;
using JobBoard.Data.Helpers.enums;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void AddJobCommandMapping()
		{
			CreateMap<AddJobCommand, JobListing>()
				.ForMember(x => x.status, opt => opt.MapFrom(src => JobStatusEnum.New))
				// this line we take user id equal to 1 just for testing,
				// we will update it later to take the user Id dynamique from header
				.ForMember(x => x.CreatedByUserId, opt => opt.MapFrom(src => 1));

		}
	}
}
