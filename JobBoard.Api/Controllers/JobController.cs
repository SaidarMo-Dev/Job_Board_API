using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]

	public class JobController : AppControllerbase
	{

		[HttpGet(Router.JobRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[AllowAnonymous]
		public async Task<IActionResult> GetJobsPaginate([FromQuery] GetPaginatedJobsQuery request)
		{
			return Ok(await Mediator.Send(request));
		}

		[HttpGet(Router.JobRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Authorize(policy: "Get")]

		public async Task<IActionResult> GetJobById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetJobByIdQuery { Id = Id }));
		}

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.Skills)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobSkills([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobSkillsQuery { JobId = JobId }));
		}

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.Categories)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobCategories([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobCategoriesQuery { JobId = JobId }));
		}

		[Authorize(Roles = "Admin,SuperAdmin")]
		[HttpPost(Router.JobRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> AddNewJob([FromBody] AddJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[Authorize(Roles = "Admin,SuperAdmin")]
		[HttpPut(Router.JobRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[Authorize(Roles = "Admin,SuperAdmin")]
		[HttpDelete(Router.JobRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteJob([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteJobCommand(Id)));
		}


		[HttpGet(Router.CompanyRoute.Jobs)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetCompanyJobs([FromRoute] int Id)
		{
			return Ok(await Mediator.Send(new GetJobsByCompanyIdQuery(Id)));
		}

	}
}
