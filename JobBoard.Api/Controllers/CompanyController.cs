using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Core.Feutures.Companies.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class CompanyController : AppControllerbase
	{

		[HttpGet(Router.CompanyRoute.Paginate)]
		public async Task<IActionResult> GetPaginatedCompanies([FromQuery] GetPaginatedListCompanyQuery query)
		{

			return Ok(await Mediator.Send(query));

		}
		[HttpGet(Router.CompanyRoute.GetByID)]
		public async Task<IActionResult> GetCompanyById([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCompanyQuery(Id)));
		}

		[HttpGet(Router.CompanyRoute.GetAll)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetAllCompaiesQuery()));
		}

		[HttpPost(Router.CompanyRoute.Create)]
		public async Task<IActionResult> AddCompany([FromBody] AddCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpPut(Router.CompanyRoute.Update)]
		public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[HttpDelete(Router.CompanyRoute.DeleteById)]
		public async Task<IActionResult> DeleteCompany([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteCompanyCommand(Id)));
		}
	}
}
