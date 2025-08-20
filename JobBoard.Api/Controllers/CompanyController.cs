using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	//[Authorize(Roles = "Admin,Employer")]
	public class CompanyController : AppControllerbase
	{
		[SwaggerOperation(
			Summary = "Get paginated companies",
			Description = "Retrieves a paginated list of companies based on the specified query parameters.",
			OperationId = "GetPaginatedCompanies")]


		[HttpGet(Router.CompanyRoute.Paginate)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> GetPaginatedCompanies([FromQuery] GetPaginatedListCompanyQuery query)
		{

			return Ok(await Mediator.Send(query));

		}


		[SwaggerOperation(
			Summary = "Get company by ID",
			Description = "Retrieves the details of a company by its unique identifier.",
			OperationId = "GetCompanyById")]

		[HttpGet(Router.CompanyRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetCompanyById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCompanyQuery(Id)));
		}


		[SwaggerOperation(
			Summary = "Get all companies",
			Description = "Returns a list of all companies. Restricted to Admin role.",
			OperationId = "GetAllCompanies")]

		//[Authorize(Roles = "Admin")]
		[HttpGet(Router.CompanyRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAll([FromQuery] GetAllCompaiesQuery query)
		{
			return Ok(await Mediator.Send(query));
		}


		[SwaggerOperation(
			Summary = "Add a new company",
			Description = "Creates a new company with the provided details.",
			OperationId = "AddCompany")]

		[HttpPost(Router.CompanyRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AddCompany([FromBody] AddCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(
			Summary = "Update an existing company",
			Description = "Updates the details of an existing company.",
			OperationId = "UpdateCompany")]


		[HttpPut(Router.CompanyRoute.Update)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}



		[SwaggerOperation(
			Summary = "Delete a company",
			Description = "Deletes a company identified by its unique ID.",
			OperationId = "DeleteCompany")]

		[HttpDelete(Router.CompanyRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]

		public async Task<IActionResult> DeleteCompany([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteCompanyCommand(Id)));
		}


		[SwaggerOperation(
			Summary = "Get popular companies",
			Description = "get most popular companies.",
			OperationId = "GetPopularCompanies")]

		[HttpGet(Router.CompanyRoute.PopularCompanies)]
		[ProducesResponseType(StatusCodes.Status200OK)]


		public async Task<IActionResult> GetPopularCompanies()
		{
			return NewResult(await Mediator.Send(new GetPopularCompaniesQuery()));
		}
	}
}
