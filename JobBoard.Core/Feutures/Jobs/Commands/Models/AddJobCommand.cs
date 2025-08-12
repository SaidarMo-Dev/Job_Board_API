using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Models
{
	public class AddJobCommand : IRequest<Response<int>>
	{
		public required string Title { get; set; }
		public string? Description { get; set; }
		public int CompanyId { get; set; }
		public required string Location { get; set; }
		public JobTypeEnum JobType { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public ExperienceLevelEnum ExperienceLevel { get; set; }

		public HashSet<int> skillIds { get; set; }
		public HashSet<int> CategoryIds { get; set; }



	}
}
