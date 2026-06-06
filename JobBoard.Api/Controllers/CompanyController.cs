using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,Employer")]

	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public class CompanyController : AppControllerbase
	{


		[SwaggerOperation(
			Summary = "Get company by ID",
			Description = "Retrieves the details of a company by its unique identifier.",
			OperationId = "GetCompanyById")]

		[AllowAnonymous]
		[HttpGet(Router.CompanyRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetCompanyById([FromRoute] int Id, [FromQuery] string? fields)
		{
			return NewResult(await Mediator.Send(new GetSingleCompanyQuery(Id, fields)));
		}


		[SwaggerOperation(
			Summary = "Get all companies",
			Description = "Returns a list of all companies. Restricted to Admin role.",
			OperationId = "GetAllCompanies")]

		[AllowAnonymous]
		[HttpGet(Router.CompanyRoute.GetCompanies)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll([FromQuery] GetCompaiesQuery query)
		{
			return Ok(await Mediator.Send(query));
		}

		[SwaggerOperation(
			Summary = "Get company by slug",
			Description = "Retrieves the details of a company by its unique slug identifier.",
			OperationId = "GetCompanyBySlug")]

		[AllowAnonymous]
		[HttpGet(Router.CompanyRoute.GetBySlug)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetCompanyBySlug([FromRoute] string slug)
		{
			return NewResult(await Mediator.Send(new GetCompanyBySlug { slug = slug }));
		}

		[SwaggerOperation(
		Summary = "Get company jobs by slug",
		Description = "Retrieves company active jobs by unique slug identifier.",
		OperationId = "GetCompanJobsBySlug")]

		[AllowAnonymous]
		[HttpGet(Router.CompanyRoute.GetJobsBySlug)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetCompanJobsBySlug([FromRoute] string slug, [FromQuery] int Page, [FromQuery] int PageSize)
		{
			return Ok(await Mediator.Send(new GetCompanyJobs { Slug = slug, Page = Page, PageSize = PageSize }));
		}


		[SwaggerOperation(
			Summary = "Get featured companies",
			Description = "Retrieves a paginated list of featured companies highlighted on the platform, including basic company information and active job count.",
			OperationId = "GetFeaturedCompanies")]

		[AllowAnonymous]
		[HttpGet(Router.CompanyRoute.GetFeaturedCompanies)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetFeaturedCompanies([FromQuery] GetFeaturedCompaniesQuery query)
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

		public async Task<IActionResult> UpdateCompany([FromRoute] int Id, [FromBody] UpdateCompanyCommand request)
		{
			request.CompanyId = Id;
			return NewResult(await Mediator.Send(request));
		}



		[SwaggerOperation(
			Summary = "Delete a company",
			Description = "Deletes a company identified by its unique ID.",
			OperationId = "DeleteCompany")]

		[HttpDelete(Router.CompanyRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public async Task<IActionResult> DeleteCompany([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteCompanyCommand(Id)));
		}

		[AllowAnonymous]
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


		[SwaggerOperation(
			Summary = "Get companies summary",
			Description = "get companies summary.",
			OperationId = "GetCompaniesSummary")]

		[HttpGet(Router.CompanyRoute.CompaniesSummary)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCompaniesSummary([FromQuery] GetCompaniesSummaryQuery quey)
		{
			return Ok(await Mediator.Send(quey));
		}

		[SwaggerOperation(
			Summary = "Update company logo",
			Description = "Upload company logo",
			OperationId = "UploadCompanyLogo")]

		[HttpPut(Router.CompanyRoute.UploadCompanyLogo)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UploadCompanyLogo([FromRoute] int Id, [FromForm] UploadCompanyLogoRequest request)
		{
			return NewResult(await Mediator.Send(new SetCompanyLogoCommand { CompanyId = Id, Logo = request.Logo }));
		}

		[SwaggerOperation(
			Summary = "Upload company banner",
			Description = "Upload company banner",
			OperationId = "UploadCompanyBanner")]

		[HttpPut(Router.CompanyRoute.UploadCompanyBanner)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UploadCompanyBanner([FromRoute] int Id, [FromForm] UploadCompanyBannerRequest request)
		{
			return NewResult(await Mediator.Send(new UploadCompanyBannerCommand(Id, request.Banner)));
		}


		[AllowAnonymous]
		[SwaggerOperation(
		Summary = "Get Company Statistics",
		Description = "Get Company Statistics",
		OperationId = "GetCompanyStatistics")]

		[HttpGet(Router.CompanyRoute.Statistics)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetStatistics()
		{
			return NewResult(await Mediator.Send(new GetCompanyStatisticsQuery()));
		}

	}
}
