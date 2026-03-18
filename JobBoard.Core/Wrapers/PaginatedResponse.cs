using System.Net;

namespace JobBoard.Core.Wrapers
{
	public class PaginatedResponse<T>
	{
		public PaginatedResponse(IEnumerable<T> data)
		{
			this.data = data;
		}
		public PaginatedResponse(IEnumerable<T> data, int page, int size, int totalRecords, string message = null)
		{
			this.data = data;
			totalPages = (int)Math.Ceiling((double)totalRecords / size);
			this.totalRecords = totalRecords;
			pageSize = size;
			this.message = message == null ? "Success" : message;
			succeeded = true;
			statusCode = HttpStatusCode.OK;
			currentPage = page > totalPages ? totalPages : page;
		}

		public static PaginatedResponse<T> Success(IEnumerable<T> data, int page, int size, int totalRecords, string message = null)
		{
			return new(data, page, size, totalRecords, message);
		}

		public IEnumerable<T> data { get; set; }
		public string message { get; set; } = string.Empty;
		public HttpStatusCode statusCode { get; set; }
		public bool succeeded { get; set; }
		public int totalPages { get; set; }
		public int totalRecords { get; set; }
		public int currentPage { get; set; }
		public int pageSize { get; set; }
		public bool hasPreviusPage => currentPage > 1;
		public bool hasNextPage => currentPage < totalPages;

	}
}
