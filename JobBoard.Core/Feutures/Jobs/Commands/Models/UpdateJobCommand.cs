using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Models
{
	public class UpdateJobCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public string? Location { get; set; }
		public JobTypeEnum JobType { get; set; }
		public ExperienceLevelEnum ExperienceLevel { get; set; }
		public double MinSalary { get; set; }
		public double MaxSalary { get; set; }
		public DateTime DateExpired { get; set; } = (DateTime.UtcNow.AddDays(10));
		public JobStatusEnum? Status { get; set; }
		public HashSet<int> SkillIds { get; set; } = new HashSet<int>();
		public HashSet<int> CategoryIds { get; set; } = new HashSet<int>();


	}
}
