using JobBoard.Data.enums;

namespace JobBoard.Core.Common.Helpers
{
	public static class CompanySizeHelper
	{
		public static string GetSize(CompanySize? size)
		{

			return size switch
			{
				CompanySize.Small => "0-50",
				CompanySize.Medium => "51-500",
				CompanySize.Large => "500+",
				_ => "0-50"
			};
		}
	}
}
