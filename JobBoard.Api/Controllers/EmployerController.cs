using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Employer.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Employer,Admin")]

	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public class EmployerController : AppControllerbase
	{
		[SwaggerOperation(
					Summary = "Get employer dashboard stats",
					Description = "Get current employer dashboard stats.",
					OperationId = "GetEmployerDashboard"
				)]

		[HttpGet(Router.EmployerRoute.dashboard)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetmployerDashboardStats()
		{
			return NewResult(await Mediator.Send(new GetEmployerDashboardStatsQuery()));
		}

		[SwaggerOperation(
					Summary = "Get employer jobs",
					Description = "Get current employer posted jobs.",
					OperationId = "GetEmployerJobs"
				)]

		[HttpGet(Router.EmployerRoute.PostedJobs)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetmployerPostedJobs([FromQuery] GetEmployerPostedJobsQuery query)
		{
			return Ok(await Mediator.Send(query));
		}

	}
}
