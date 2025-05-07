using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Skills.Commands.Models;
using JobBoard.Core.Feutures.Skills.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[AllowAnonymous]
	public class SkillController : AppControllerbase
	{
		[HttpGet(Router.SkillRoute.GetByID)]
		public async Task<IActionResult> GetSkillById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleSkillQuery(Id)));

		}
		[HttpGet(Router.SkillRoute.GetAll)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListSkillsQuery()));

		}

		[HttpPost(Router.SkillRoute.Create)]
		public async Task<IActionResult> AddNewSkill([FromBody] AddSkillCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[HttpPut(Router.SkillRoute.Update)]
		public async Task<IActionResult> UpdateSkill([FromBody] UpdateSkillCommand request)
		{
			return NewResult(await Mediator.Send(request));

		}

		[HttpDelete(Router.SkillRoute.DeleteById)]
		public async Task<IActionResult> DeleteSkill([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteSkillCommand { Id = Id }));

		}

	}
}
