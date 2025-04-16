namespace JobBoard.Core.Bases
{
	public class ResponseHandler
	{
		public Response<T> Deleted<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Message = "Deleted Successfully",
				Succeeded = true
			};
		}
		public Response<T> Deleted<T>(T data)
		{
			return new Response<T>()
			{
				Data = data,
				StatusCode = System.Net.HttpStatusCode.OK,
				Message = "Deleted Successfully",
				Succeeded = true
			};
		}
		public Response<T> NotFound<T>(string message = "Not Found")
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Message = message,
				Succeeded = false,

			};
		}

		public Response<TEntity> NotFound<TEntity>(TEntity entity, string message = null!)
		{
			return new Response<TEntity>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Message = message == null ? "Not Found" : message,
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
				Message = message is null ? "Completed Successfully" : message,
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
