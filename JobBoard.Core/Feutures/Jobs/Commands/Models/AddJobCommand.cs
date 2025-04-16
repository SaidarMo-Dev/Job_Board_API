using JobBoard.Core.Bases;
using JobBoard.Data.Helpers.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Commands.Models
{
	public class AddJobCommand : IRequest<Response<int>>
	{
		public required string Title { get; set; }
		public string? Description { get; set; }
		public int CompanyId { get; set; }
		public string? Location { get; set; }
		public JobTypeEnum JobType { get; set; }
		public double SalaryRange { get; set; }
		public DateTime DatePosted { get; set; }

		public List<int> skillsId { get; set; }
		public List<int> CategoriesId { get; set; }


	}
}
