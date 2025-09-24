using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]

	[Authorize(Roles = "Admin,Employer")]
	public class JobController : AppControllerbase
	{

		[SwaggerOperation(
			Summary = "Get paginated jobs",
			Description = "Returns a paginated list of jobs based on the query parameters.",
			OperationId = "GetJobsPaginate")]

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobsPaginate([FromQuery] GetPaginatedJobsQuery request)
		{
			return Ok(await Mediator.Send(request));
		}

		[SwaggerOperation(
			Summary = "Get job by ID",
			Description = "Retrieves detailed information about a specific job identified by its ID.",
			OperationId = "GetJobById")]

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetJobById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetJobByIdQuery { Id = Id }));
		}


		[SwaggerOperation(
			Summary = "Get job by Id summary",
			Description = "Retrieves a summary information about a specific job identified by its Id.",
			OperationId = "GetJobByIdSummary")]

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.GetByIDSummary)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetJobByIdSummary([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetJobByIdSummaryQuery { Id = Id }));
		}


		[SwaggerOperation(
			Summary = "Get job skills",
			Description = "Returns a list of skills associated with a specific job identified by its JobId.",
			OperationId = "GetJobSkills")]

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.Skills)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobSkills([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobSkillsQuery { JobId = JobId }));
		}



		[SwaggerOperation(
			Summary = "Get job categories",
			Description = "Returns a list of categories associated with a specific job identified by its JobId.",
			OperationId = "GetJobCategories")]

		[AllowAnonymous]
		[HttpGet(Router.JobRoute.Categories)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetJobCategories([FromQuery] int JobId)
		{
			return NewResult(await Mediator.Send(new GetJobCategoriesQuery { JobId = JobId }));
		}


		[SwaggerOperation(
			Summary = "Create a new job",
			Description = "Creates a new job entry with the provided job details.",
			OperationId = "AddNewJob")]

		[HttpPost(Router.JobRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> AddNewJob([FromBody] AddJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(
			Summary = "Update a job",
			Description = "Updates an existing job with the provided information.",
			OperationId = "UpdateJob")]

		[Authorize(Roles = "Employer")]
		[HttpPut(Router.JobRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[SwaggerOperation(
			Summary = "Delete a job",
			Description = "Deletes a job identified by its unique ID.",
			OperationId = "DeleteJob")]

		[HttpDelete(Router.JobRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> DeleteJob([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteJobCommand(Id)));
		}

		[SwaggerOperation(
			Summary = "Get jobs by company ID",
			Description = "Retrieves all jobs posted by a specific company identified by its ID.",
			OperationId = "GetCompanyJobs")]

		[HttpGet(Router.CompanyRoute.Jobs)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetCompanyJobs([FromRoute] int Id)
		{
			return Ok(await Mediator.Send(new GetJobsByCompanyIdQuery(Id)));
		}

		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Get popular locations",
			Description = "Retrieves popular locations.",
			OperationId = "GetPopularLocations")]

		[HttpGet(Router.JobRoute.Locations)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetPopularLocations()
		{
			return NewResult(await Mediator.Send(new GetPopularLocationsQuery()));
		}



		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Get recommendation jobs",
			Description = "Retreives recommendation jobs for loged user",
			OperationId = "GetRecommendationJobs")]

		[HttpGet(Router.JobRoute.Recommendations)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetRecommendationJobs()
		{
			return NewResult(await Mediator.Send(new GetRecommendationJobsQuery()));
		}



	}
}
