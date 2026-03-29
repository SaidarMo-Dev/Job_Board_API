using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Industry.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{

	[ApiController]

	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public class IndustryController : AppControllerbase
	{
		[HttpGet(Router.IndustryRoute.GetAll)]
		[SwaggerOperation(Summary = "Get industries",
				  Description = "Retrieves industries.",
				  OperationId = "GetIndustries")]

		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetIndustries([FromQuery] GetIndustriesQuery query)
		{
			var result = await Mediator.Send(query);
			return Ok(result);
		}
	}
}
