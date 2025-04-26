using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize]
	public class JobController : AppControllerbase
	{

		[HttpGet(Router.JobRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetJobsPaginate([FromQuery] GetPaginatedJobsQuery request)
		{
			return Ok(await Mediator.Send(request));
		}


		[HttpGet(Router.JobRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetJobById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetJobByIdQuery { Id = Id }));
		}


		[HttpGet(Router.JobRoute.Skills)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobSkills([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobSkillsQuery { JobId = JobId }));
		}


		[HttpGet(Router.JobRoute.Categories)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobCategories([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobCategoriesQuery { JobId = JobId }));
		}


		[HttpPost(Router.JobRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> AddNewJob([FromBody] AddJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[HttpPut(Router.JobRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpDelete(Router.JobRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteJob([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteJobCommand(Id)));
		}
	}
}
