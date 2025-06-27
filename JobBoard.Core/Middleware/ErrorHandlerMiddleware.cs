using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using JobBoard.Core.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace JobBoard.Core.Middleware
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;

		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				var responseModel = new Response<string>() { succeeded = false, message = error?.Message };

				switch (error)
				{

					case ArgumentNullException e:

						responseModel.message = e.Message;
						responseModel.message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

						responseModel.statusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						Log.Error(e, "Error : " + responseModel.message);
						break;


					case UnauthorizedAccessException e:

						responseModel.message = error.Message;
						responseModel.statusCode = HttpStatusCode.Unauthorized;
						response.StatusCode = (int)HttpStatusCode.Unauthorized;
						Log.Error(e, "Error : " + responseModel.message);

						break;

					case ValidationException e:

						responseModel.message = error.Message;
						responseModel.statusCode = HttpStatusCode.UnprocessableEntity;
						response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
						Log.Error(e, "Error : " + responseModel.message);

						break;
					case KeyNotFoundException e:

						responseModel.message = error.Message; ;
						responseModel.statusCode = HttpStatusCode.NotFound;
						response.StatusCode = (int)HttpStatusCode.NotFound;
						Log.Error(e, "Error : " + responseModel.message);

						break;

					case DbUpdateException e:

						responseModel.message = e.Message;
						responseModel.statusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						Log.Error(e, "Error : " + responseModel.message);

						break;

					case Exception e:

						responseModel.message = e.Message;
						responseModel.message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

						responseModel.statusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						Log.Error(e, "Error : " + responseModel.message);
						break;



					default:
						// unhandled error
						responseModel.message = error?.Message;
						responseModel.statusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						Log.Error(error, "Error : " + responseModel.message);
						break;
				}
				var result = JsonSerializer.Serialize(responseModel);

				await response.WriteAsync(result);
			}
		}
	}
}
