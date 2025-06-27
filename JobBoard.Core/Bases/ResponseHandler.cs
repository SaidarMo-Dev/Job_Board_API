using JobBoard.Core.Resources;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}

		public Response<T> Deleted<T>()
		{
			return new Response<T>()
			{
				statusCode = System.Net.HttpStatusCode.OK,
				message = _stringLocalizer[SharedResourcesKeys.Deleted],
				succeeded = true
			};
		}
		public Response<T> Deleted<T>(T data)
		{
			return new Response<T>()
			{
				data = data,
				statusCode = System.Net.HttpStatusCode.OK,
				message = _stringLocalizer[SharedResourcesKeys.Deleted],
				succeeded = true
			};
		}
		public Response<T> BadRequest<T>(string message = null)
		{
			return new Response<T>()
			{
				statusCode = System.Net.HttpStatusCode.NotFound,
				message = message is null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message,
				succeeded = false,

			};
		}

		public Response<TEntity> NotFound<TEntity>(TEntity entity, string message = null!)
		{
			return new Response<TEntity>()
			{
				data = entity,
				statusCode = System.Net.HttpStatusCode.NotFound,
				message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message,
				succeeded = false
			};
		}


		public Response<TEntity> Success<TEntity>(TEntity entity, string message = null!)
		{
			return new Response<TEntity>()
			{
				data = entity,
				statusCode = System.Net.HttpStatusCode.OK,
				succeeded = true,
				message = message is null ? _stringLocalizer[SharedResourcesKeys.Success] : message,

			};
		}
		public Response<TEntity> Success<TEntity>(string message = null!, object meta = null)
		{
			return new Response<TEntity>()
			{

				statusCode = System.Net.HttpStatusCode.OK,
				succeeded = true,
				message = message is null ? _stringLocalizer[SharedResourcesKeys.Success] : message,

			};
		}

		public Response<TEntity> Unauthorized<TEntity>(string message = null!)
		{
			return new Response<TEntity>()
			{
				statusCode = System.Net.HttpStatusCode.Unauthorized,
				message = message is null ? "Unauthorized" : message,
				succeeded = false
			};
		}

		public Response<TEntity> Forbidden<TEntity>(string message = null!)
		{
			return new Response<TEntity>()
			{
				statusCode = System.Net.HttpStatusCode.Forbidden,
				message = message is null ? "Forbidden" : message,
				succeeded = false
			};
		}


		public Response<TEntity> BadRequest<TEntity>(TEntity data, string message = null)
		{
			return new Response<TEntity>()
			{
				data = data,
				statusCode = System.Net.HttpStatusCode.BadRequest,
				message = message == null ? "Bad Request" : message,
				succeeded = false
			};
		}
		public Response<TEntity> NotFound<TEntity>(string message = null)
		{
			return new Response<TEntity>()
			{

				statusCode = System.Net.HttpStatusCode.NotFound,
				message = message == null ? "Bad Request" : message,
				succeeded = false
			};
		}

		public Response<TEntity> Created<TEntity>(TEntity entity)
		{
			return new Response<TEntity>()
			{
				data = entity,
				statusCode = System.Net.HttpStatusCode.Created,
				succeeded = true,
				message = "Created Successsfuly"
			};
		}


	}
}
