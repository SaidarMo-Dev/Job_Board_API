using System.Net;
using JobBoard.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Api.Bases
{
	public class AppControllerbase : ControllerBase
	{

		private IMediator _mediatorInstance;
		protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

		#region Actions

		public ObjectResult NewResult<T>(Response<T> response)
		{
			switch (response.statusCode)
			{
				case HttpStatusCode.OK:
					return new OkObjectResult(response);

				case HttpStatusCode.Created:
					return new CreatedResult(string.Empty, response);

				case HttpStatusCode.Unauthorized:
					return new UnauthorizedObjectResult(response);

				case HttpStatusCode.BadRequest:
					return new BadRequestObjectResult(response);

				case HttpStatusCode.NotFound:
					return new NotFoundObjectResult(response);

				case HttpStatusCode.Accepted:
					return new AcceptedResult(string.Empty, response);

				case HttpStatusCode.UnprocessableEntity:
					return new UnprocessableEntityObjectResult(response);
				case HttpStatusCode.Forbidden:
					return new ObjectResult(response)
					{
						StatusCode = StatusCodes.Status403Forbidden
					};


				default:
					return new BadRequestObjectResult(response);

			}
		}
		#endregion
	}
}
