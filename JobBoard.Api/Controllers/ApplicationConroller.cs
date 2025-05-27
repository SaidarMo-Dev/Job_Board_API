using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,User")]
	public class ApplicationController : AppControllerbase
	{
		[SwaggerOperation(summary: "Get Application")]
		[HttpGet(Router.ApplicationRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetApplication([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleApplicationQuery { Id = Id }));
		}


		// current User Applications
		[SwaggerOperation(summary: "User Applications")]
		[Authorize(Roles = "JobSeeker")]

		[HttpGet(Router.ApplicationUserRoute.Applications)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CurrentUserApplications()
		{
			return NewResult(await Mediator.Send(new GetCurrentUserApplicationsQuery()));

		}


		// list Applications for a job
		[SwaggerOperation(summary: "Job Applications")]
		[Authorize(Roles = "Admin,Employer")]
		[HttpGet(Router.JobRoute.Applications)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> JobApplications([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetApplicationsByJobIdQuery(Id)));

		}

		// apply for a job
		[SwaggerOperation(summary: "Apply For a job")]
		[AllowAnonymous]
		[HttpPost(Router.ApplicationRoute.Apply)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ApplyForJob([FromBody] AddApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(summary: "Update Application")]
		[Authorize(Roles = "Admin")]

		[HttpPut(Router.ApplicationRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(summary: "Update Status")]
		[Authorize(Roles = "Admin,Employer")]
		[HttpPut(Router.ApplicationRoute.SetAccepted)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToAccepted([FromQuery] SetApplicationStatusToAcceptedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[Authorize(Roles = "Admin,Employer")]
		[SwaggerOperation(summary: "Update Status")]
		[HttpPut(Router.ApplicationRoute.SetRemoved)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToRemoved([FromQuery] SetApplicationStatusToRemovedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[Authorize(Roles = "Admin,Employer")]
		[SwaggerOperation(summary: "Delete Application")]
		[HttpDelete(Router.ApplicationRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> DeleteApplication([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteApplicationCommand { Id = Id }));
		}


	}
}
