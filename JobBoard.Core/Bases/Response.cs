using System.Net;

namespace JobBoard.Core.Bases
{
	public class Response<T>
	{

		public HttpStatusCode statusCode { get; set; }
		public bool succeeded { get; set; }
		public string message { get; set; }
		public List<string> errors { get; set; }
		public T data { get; set; }
	}
}
