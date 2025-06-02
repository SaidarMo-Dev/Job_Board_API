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
		[SwaggerOperation(
				Summary = "Get application by ID",
				Description = "Retrieves a specific application by its unique identifier.",
				OperationId = "GetApplicationById"
			)]

		[HttpGet(Router.ApplicationRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetApplication([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleApplicationQuery { Id = Id }));
		}


		// current User Applications

		[SwaggerOperation(Summary = "Get current user applications",
						  Description = "Retrieves a list of applications submitted by the currently logged-in user.",
						  OperationId = "GetCurrentUserApplications")]

		[Authorize(Roles = "JobSeeker,Employer")]
		[HttpGet(Router.ApplicationUserRoute.Applications)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CurrentUserApplications()
		{
			return NewResult(await Mediator.Send(new GetCurrentUserApplicationsQuery()));

		}


		// list Applications for a job

		[SwaggerOperation(
				Summary = "Get job applications",
				Description = "Retrieves all applications submitted for a specific job.",
				OperationId = "GetApplicationsByJobId"
			)]

		[Authorize(Roles = "Admin,Employer")]
		[HttpGet(Router.JobRoute.Applications)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> JobApplications([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetApplicationsByJobIdQuery(Id)));

		}

		// apply for a job

		[SwaggerOperation(
					Summary = "Apply for a job",
					Description = "Allows a user to submit a job application.",
					OperationId = "ApplyForJob"
			)]

		[AllowAnonymous]
		[HttpPost(Router.ApplicationRoute.Apply)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ApplyForJob([FromBody] AddApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Update application",
						  Description = "Updates the information of an existing application.",
						  OperationId = "UpdateApplication")]

		[Authorize(Roles = "Admin")]
		[HttpPut(Router.ApplicationRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}



		[SwaggerOperation(Summary = "Set application status to accepted",
						  Description = "Updates the status of an application to 'Accepted'.",
						  OperationId = "SetApplicationStatusToAccepted")]

		[Authorize(Roles = "Admin,Employer")]
		[HttpPut(Router.ApplicationRoute.SetAccepted)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToAccepted([FromQuery] SetApplicationStatusToAcceptedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Set application status to removed",
						  Description = "Updates the status of an application to 'Removed'.",
						  OperationId = "SetApplicationStatusToRemoved")]

		[Authorize(Roles = "Admin,Employer")]
		[HttpPut(Router.ApplicationRoute.SetRemoved)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToRemoved([FromQuery] SetApplicationStatusToRemovedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(Summary = "Delete application",
						  Description = "Deletes an application by its ID.",
						  OperationId = "DeleteApplicationById")]

		[Authorize(Roles = "Admin,Employer")]
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
