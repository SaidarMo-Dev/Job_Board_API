using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Skills.Commands.Models;
using JobBoard.Core.Feutures.Skills.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,Employer")]
	public class SkillController : AppControllerbase
	{

		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Get skill by ID",
			Description = "Retrieves detailed information about a specific skill identified by its ID.",
			OperationId = "GetSkillById")]

		[HttpGet(Router.SkillRoute.GetByID)]
		public async Task<IActionResult> GetSkillById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleSkillQuery(Id)));

		}

		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Get all skills",
			Description = "Returns a list of all skills available in the system.",
			OperationId = "GetAllSkills")]

		[HttpGet(Router.SkillRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public async Task<IActionResult> GetAll([FromQuery] GetListSkillsQuery query)
		{
			return Ok(await Mediator.Send(query));

		}


		[AllowAnonymous]
		[SwaggerOperation(
		Summary = "Get skills summary",
		Description = "Returns a summary list of skills available in the system.",
		OperationId = "GetSkillsSummary")]

		[HttpGet(Router.SkillRoute.Summary)]

		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetSkillsSummary([FromQuery] GetSkillsSummaryQuery query)
		{
			return Ok(await Mediator.Send(query));
		}


		[SwaggerOperation(
			Summary = "Create a new skill",
			Description = "Adds a new skill to the system with the provided details.",
			OperationId = "AddNewSkill")]

		[HttpPost(Router.SkillRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> AddNewSkill([FromBody] AddSkillCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[SwaggerOperation(
			Summary = "Update a skill",
			Description = "Updates an existing skill with the provided information.",
			OperationId = "UpdateSkill")]

		[Authorize(Roles = "Admin")]
		[HttpPut(Router.SkillRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public async Task<IActionResult> UpdateSkill([FromBody] UpdateSkillCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}


		[SwaggerOperation(
			Summary = "Delete a skill",
			Description = "Deletes a skill identified by its unique ID.",
			OperationId = "DeleteSkill")]

		[Authorize(Roles = "Admin")]
		[HttpDelete(Router.SkillRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public async Task<IActionResult> DeleteSkill([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteSkillCommand { Id = Id }));

		}

	}
}
