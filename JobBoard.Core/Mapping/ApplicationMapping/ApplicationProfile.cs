using AutoMapper;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile : Profile
	{
		public ApplicationProfile()
		{
			AddApplicationMapping();
			UpdateApplicationMapping();
			GetSingleApplicationMapping();
		}
	}
}
