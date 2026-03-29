using AutoMapper;

namespace JobBoard.Core.Mapping.IndustryMapping
{
	public partial class IndustryProfile : Profile
	{
		public IndustryProfile()
		{
			MapGetIndustries();
		}
	}
}
