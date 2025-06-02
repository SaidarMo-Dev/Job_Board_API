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

		[SwaggerOperation(
			Summary = "Get skill by ID",
			Description = "Retrieves detailed information about a specific skill identified by its ID.",
			OperationId = "GetSkillById")]

		[HttpGet(Router.SkillRoute.GetByID)]
		public async Task<IActionResult> GetSkillById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleSkillQuery(Id)));

		}

		[SwaggerOperation(
			Summary = "Get all skills",
			Description = "Returns a list of all skills available in the system.",
			OperationId = "GetAllSkills")]

		[HttpGet(Router.SkillRoute.GetAll)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListSkillsQuery()));

		}


		[SwaggerOperation(
			Summary = "Create a new skill",
			Description = "Adds a new skill to the system with the provided details.",
			OperationId = "AddNewSkill")]

		[HttpPost(Router.SkillRoute.Create)]
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
		public async Task<IActionResult> DeleteSkill([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteSkillCommand { Id = Id }));

		}

	}
}
