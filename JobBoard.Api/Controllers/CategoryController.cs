using JobBoard.Api.Bases;
using JobBoard.Core.Feutures.Categories.Commands.Models;
using JobBoard.Core.Feutures.Categories.Queries.Models;
using JobBoard.Data.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobBoard.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,Employer")]
	public class CategoryController : AppControllerbase
	{
		[SwaggerOperation(Summary = "Get category by ID",
				  Description = "Retrieves a specific category using its unique identifier.",
				  OperationId = "GetCategoryByID")]

		[HttpGet(Router.CategoryRoute.GetByID)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<ActionResult> GetCategoryByID([FromRoute] int Id)
		{
			return NewResult(await Mediator.Send(new GetSingleCategoryQuery(Id)));
		}


		[SwaggerOperation(Summary = "Get all categories",
				  Description = "Returns a list of all categories stored in the system.",
				  OperationId = "GetAllCategories")]

		[HttpGet(Router.CategoryRoute.GetAll)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> GetAll()
		{
			return NewResult(await Mediator.Send(new GetListCategoriesQuery()));
		}

		[SwaggerOperation(Summary = "Create a new category",
				  Description = "Creates a new category entry in the system.",
				  OperationId = "CreateCategory")]

		[HttpPost(Router.CategoryRoute.Create)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> CreateCategory([FromBody] AddCategoryCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}


		[Authorize(Roles = "Admin")]

		[SwaggerOperation(Summary = "Update a category (Admin only)",
				  Description = "Updates the details of an existing category. Requires admin privileges.",
				  OperationId = "UpdateCategory")]

		[HttpPut(Router.CategoryRoute.Update)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
		{
			return NewResult(await Mediator.Send(request));
		}

		[SwaggerOperation(Summary = "Delete a category by ID (Admin only)",
				  Description = "Deletes a specific category by its unique identifier. Requires admin privileges.",
				  OperationId = "DeleteCategory")]

		[Authorize(Roles = "Admin")]
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
