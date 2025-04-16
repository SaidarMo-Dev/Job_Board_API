using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Categories.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	public class CategoryController : AppControllerbase
	{

		[HttpGet(Router.CategoryRoute.GetByID)]
		public async Task<ActionResult> GetCategoryByID([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCategoryQuery(Id)));
		}

		[HttpGet(Router.CategoryRoute.GetAll)]
		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListCategoriesQuery()));
		}
	}
}
