using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void UpdateJobCommandMapping()
		{
			CreateMap<UpdateJobCommand, JobListing>();
		}
	}
}
