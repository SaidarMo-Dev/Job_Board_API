using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Countries.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[AllowAnonymous]
	public class CountryController : AppControllerbase
	{
		[SwaggerOperation(
			summary: "Retrieves Country Details by Id",
			OperationId = "GetCountryById",
			Description = "This EndPoint fetch Country Details. Country must exist"

		)]

		[HttpGet(Router.CountryRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> FindCountryById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetCountryByIdQuery(Id)));
		}
		[AllowAnonymous]
		[SwaggerOperation(
			Summary = "Get all countries",
			Description = "Returns a list of all countries stored in the system.",
			OperationId = "GetAllCountries"
		)]

		[HttpGet(Router.CountryRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListCountriesQuery()));
		}
	}
}
