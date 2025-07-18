using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JobBoard.Infrastructure.ModelBinders
{
	public class CaseInsensitiveFormModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			if (context.Metadata.IsComplexType &&
				context.BindingInfo.BindingSource?.Id == "Form")
			{
				return new CaseInsensitiveFormModelBinder();
			}
			return null;
		}
	}
}
