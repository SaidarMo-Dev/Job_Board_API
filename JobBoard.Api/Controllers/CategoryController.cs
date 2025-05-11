using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Categories.Commands.Models;
using JobBoard.Core.Feutures.Categories.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class CategoryController : AppControllerbase
	{

		[HttpGet(Router.CategoryRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<ActionResult> GetCategoryByID([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCategoryQuery(Id)));
		}


		[HttpGet(Router.CategoryRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListCategoriesQuery()));
		}

		[HttpPost(Router.CategoryRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> CreateCategory([FromBody] AddCategoryCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[HttpPut(Router.CategoryRoute.Update)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[HttpDelete(Router.CategoryRoute.DeleteById)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> DeleteCategory([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new DeleteCategoryCommand { Id = Id }));

		}
	}
}
