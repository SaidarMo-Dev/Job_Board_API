using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile
	{
		public void MapCompanyPreview()
		{
			CreateMap<Company, CompanyPreviewDto>()
			.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
			.ForMember(dest => dest.LogoUrl, opt =>
				opt.MapFrom(src => src.LogoFile != null ? src.LogoFile.Path : null));

		}
	}
}
