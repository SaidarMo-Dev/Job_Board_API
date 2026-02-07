using FluentValidation;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Feutures.Files.Validations;
using JobBoard.Data.Helpers;
using Microsoft.Extensions.Options;

namespace JobBoard.Core.Feutures.Files.Commands.Validations
{
	public class UpdoadFileCommandValidator : AbstractValidator<UploadFileCommand>
	{

		public UpdoadFileCommandValidator(IOptions<SupabaseSettings> options)
		{
			var supabaseSettings = options.Value;

			RuleFor(x => x.File)
							.NotEmptyFile()
							.WithMaxSize(supabaseSettings.MaxFileSizeBytes)
							.WithAllowedExtensions(supabaseSettings.AllowedExtensions)
							.WithAllowedContentTypes(supabaseSettings.AllowedContentTypes)
							.HasSafeFileName()
							.SingleExtension();
		}

	}


}



