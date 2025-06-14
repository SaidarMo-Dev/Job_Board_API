using System.Net;

namespace JobBoard.Core.Bases
{
	public class Response<T>
	{

		public HttpStatusCode StatusCode { get; set; }
		public bool Succeeded { get; set; }
		public string Message { get; set; }
		public List<string> Errors { get; set; }
		public T Data { get; set; }
	}
}
