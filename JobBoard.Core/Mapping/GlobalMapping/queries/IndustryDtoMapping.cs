using JobBoard.Core.Common.DTOs;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile
	{
		public void MapIndustryDto()
		{
			CreateMap<Industry, IndustryDto>();
		}
	}
}
