using System.Net;

namespace JobBoard.Core.Wrapers
{
	public class PaginatedResponse<T>
	{
		public PaginatedResponse(T data)
		{
			Data = data;
		}
		public PaginatedResponse(T data, int page, int size, int totalRecords, string message = null)
		{
			Data = data;
			TotalPages = (int)Math.Ceiling((double)totalRecords / size);
			TotalRecords = totalRecords;
			PageSize = size;
			Message = message == null ? "Success" : message;
			Succeeded = true;
			StatusCode = HttpStatusCode.OK;
			CurrentPage = page > TotalPages ? TotalPages : page;
		}

		public static PaginatedResponse<T> Success(T data, int page, int size, int totalRecords, string message = null)
		{
			return new(data, page, size, totalRecords, message);
		}

		public T Data { get; set; }
		public string Message { get; set; } = string.Empty;
		public HttpStatusCode StatusCode { get; set; }
		public bool Succeeded { get; set; }
		public int TotalPages { get; set; }
		public int TotalRecords { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public bool HasPreviusPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPages;

	}
}
