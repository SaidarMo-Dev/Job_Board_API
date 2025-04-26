using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Models
{
	public class AddJobCommand : IRequest<Response<int>>
	{
		public required string Title { get; set; }
		public string? Description { get; set; }
		public string? Location { get; set; }
		public int CompanyId { get; set; }
		public JobTypeEnum JobType { get; set; }
		public string SalaryRange { get; set; }
		public DateTime DatePosted { get; set; }

		public HashSet<int> skillsId { get; set; }
		public HashSet<int> CategoriesId { get; set; }



	}
}
