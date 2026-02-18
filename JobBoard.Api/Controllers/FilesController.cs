using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Files.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]

	[Authorize]

	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public class FilesController : AppControllerbase
	{

		[Authorize]
		[SwaggerOperation(Summary = "Generate Signed Url",
				  Description = "Generate signed url for file resource.",
				  OperationId = "GenerateSignedUrl")]

		[HttpGet(Router.FilesRoute.SignedUrl)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public async Task<IActionResult> GenerateSignedUrl([FromRoute] int Id, [FromQuery] bool Download)
		{
			return Ok(await Mediator.Send(new GenerateFileAccessUrlQuery { FileResourceId = Id, Download = Download }));
		}

	}
}
