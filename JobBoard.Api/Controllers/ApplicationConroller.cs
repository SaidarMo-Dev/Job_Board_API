using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class ApplicationController : AppControllerbase
	{

		[HttpGet(Router.ApplicationRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> AddApplication([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleApplicationQuery { Id = Id }));
		}

		[HttpPost(Router.ApplicationRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> AddApplication([FromBody] AddApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.ApplicationRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.ApplicationRoute.SetAccepted)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToAccepted([FromQuery] SetApplicationStatusToAcceptedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[HttpPut(Router.ApplicationRoute.SetRemoved)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> SetToRemoved([FromQuery] SetApplicationStatusToRemovedCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

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
