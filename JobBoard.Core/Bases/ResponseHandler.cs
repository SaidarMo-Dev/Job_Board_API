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
				StatusCode = System.Net.HttpStatusCode.OK,
				Message = _stringLocalizer[SharedResourcesKeys.Deleted],
				Succeeded = true
			};
		}
		public Response<T> Deleted<T>(T data)
		{
			return new Response<T>()
			{
				Data = data,
				StatusCode = System.Net.HttpStatusCode.OK,
				Message = _stringLocalizer[SharedResourcesKeys.Deleted],
				Succeeded = true
			};
		}
		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Message = message is null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message,
				Succeeded = false,

			};
		}

		public Response<TEntity> NotFound<TEntity>(TEntity entity, string message = null!)
		{
			return new Response<TEntity>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message,
				Succeeded = false
			};
		}


		public Response<TEntity> Success<TEntity>(TEntity entity, string message = null!, object meta = null)
		{
			return new Response<TEntity>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = message is null ? _stringLocalizer[SharedResourcesKeys.Completed] : message,
				Meta = meta
			};
		}
		public Response<TEntity> Success<TEntity>(string message = null!, object meta = null)
		{
			return new Response<TEntity>()
			{

				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = message is null ? _stringLocalizer[SharedResourcesKeys.Completed] : message,
				Meta = meta
			};
		}

		public Response<TEntity> Unauthorized<TEntity>(string message = null!)
		{
			return new Response<TEntity>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Message = message is null ? "Unauthorized" : message,
				Succeeded = false
			};
		}

		public Response<TEntity> BadRequest<TEntity>(TEntity data, string message = null)
		{
			return new Response<TEntity>()
			{
				Data = data,
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Message = message == null ? "Bad Request" : message,
				Succeeded = false
			};
		}
		public Response<TEntity> BadRequest<TEntity>(string message = null)
		{
			return new Response<TEntity>()
			{

				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Message = message == null ? "Bad Request" : message,
				Succeeded = false
			};
		}

		public Response<TEntity> Created<TEntity>(TEntity entity, object meta = null!)
		{
			return new Response<TEntity>()
			{
				Data = entity,
				Meta = meta,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = "Created Successsfuly"
			};
		}


	}
}
