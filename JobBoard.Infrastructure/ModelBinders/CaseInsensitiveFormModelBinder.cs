using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;

namespace JobBoard.Infrastructure.ModelBinders
{
	public class CaseInsensitiveFormModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			try
			{
				var form = bindingContext.HttpContext?.Request.Form;
				if (form == null)
				{
					bindingContext.Result = ModelBindingResult.Failed();
					return Task.CompletedTask;
				}

				var model = Activator.CreateInstance(bindingContext.ModelType)!;

				foreach (var property in bindingContext.ModelType.GetProperties())
				{
					try
					{
						if (property.PropertyType == typeof(IFormFile))
						{
							var file = form.Files.GetFile(property.Name);
							if (file != null)
							{
								property.SetValue(model, file);
							}
						}
						else
						{
							if (form.TryGetValue(property.Name, out var value))
							{
								var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
								object? convertedValue = null;

								if (targetType.IsEnum)
								{
									convertedValue = Enum.Parse(targetType, value.ToString());
								}
								else if (targetType == typeof(Guid))
								{
									convertedValue = Guid.Parse(value.ToString());
								}
								else
								{
									convertedValue = Convert.ChangeType(value.ToString(), targetType);
								}

								property.SetValue(model, convertedValue);
							}
						}
					}
					catch (Exception ex)
					{
						Log.Error($"Failed to bind property '{property.Name}'", ex);
						throw new InvalidOperationException($"Failed to bind property '{property.Name}'", ex);
					}
				}

				bindingContext.Result = ModelBindingResult.Success(model);
			}
			catch (Exception ex)
			{
				bindingContext.Result = ModelBindingResult.Failed();
				Log.Error("Model binding failed", ex);
				throw;
			}

			return Task.CompletedTask;
		}
	}
}
