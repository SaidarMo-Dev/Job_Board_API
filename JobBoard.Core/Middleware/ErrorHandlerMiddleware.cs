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

				var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

				switch (error)
				{

					case ArgumentNullException e:

						responseModel.Message = e.Message;
						responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

						responseModel.StatusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						Log.Error(e, "Error : " + responseModel.Message);
						break;


					case UnauthorizedAccessException e:

						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.Unauthorized;
						response.StatusCode = (int)HttpStatusCode.Unauthorized;
						Log.Error(e, "Error : " + responseModel.Message);

						break;

					case ValidationException e:

						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
						response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
						Log.Error(e, "Error : " + responseModel.Message);

						break;
					case KeyNotFoundException e:

						responseModel.Message = error.Message; ;
						responseModel.StatusCode = HttpStatusCode.NotFound;
						response.StatusCode = (int)HttpStatusCode.NotFound;
						Log.Error(e, "Error : " + responseModel.Message);

						break;

					case DbUpdateException e:

						responseModel.Message = e.Message;
						responseModel.StatusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						Log.Error(e, "Error : " + responseModel.Message);

						break;

					case Exception e:

						responseModel.Message = e.Message;
						responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

						responseModel.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						Log.Error(e, "Error : " + responseModel.Message);
						break;



					default:
						// unhandled error
						responseModel.Message = error?.Message;
						responseModel.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						Log.Error(error, "Error : " + responseModel.Message);
						break;
				}
				var result = JsonSerializer.Serialize(responseModel);

				await response.WriteAsync(result);
			}
		}
	}
}
