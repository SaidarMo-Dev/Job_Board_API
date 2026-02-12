
using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Files.Queries.Models
{
	public class GenerateFileAccessUrlQuery : IRequest<Response<string>>
	{
		public int FileResourceId { get; set; }
	}
}
