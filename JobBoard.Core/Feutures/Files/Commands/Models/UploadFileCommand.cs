using JobBoard.Core.Bases;
using JobBoard.Data.enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Core.Feutures.Files.Commands.Models
{

	public record UploadFileCommand(
	IFormFile File,
	FileOwnerType OwnerType,
	int OwnerId,
	FileVisibility Visibility,
	FilePathType FilePathType) : IRequest<Response<int>>;

}
