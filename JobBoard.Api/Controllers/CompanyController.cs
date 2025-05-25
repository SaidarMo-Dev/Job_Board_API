using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]

	public class CompanyController : AppControllerbase
	{
		[Authorize(Roles = "Admin")]
		[HttpGet(Router.CompanyRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetPaginatedCompanies([FromQuery] GetPaginatedListCompanyQuery query)
		{

			return Ok(await Mediator.Send(query));

		}
		[Authorize(Roles = "User")]
		[HttpGet(Router.CompanyRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetCompanyById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCompanyQuery(Id)));
		}
		[Authorize(Roles = "Admin")]
		[HttpGet(Router.CompanyRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetAllCompaiesQuery()));
		}

		[Authorize(Roles = "Admin, SuperAdmin")]
		[HttpPost(Router.CompanyRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AddCompany([FromBody] AddCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}
		[Authorize(Roles = "Admin")]
		[HttpPut(Router.CompanyRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete(Router.CompanyRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> DeleteCompany([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteCompanyCommand(Id)));
		}
	}
}
