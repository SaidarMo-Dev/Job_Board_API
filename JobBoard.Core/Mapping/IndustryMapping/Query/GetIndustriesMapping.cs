using JobBoard.Core.Feutures.Industry.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.IndustryMapping
{
	public partial class IndustryProfile
	{
		public void MapGetIndustries()
		{
			CreateMap<Industry, GetIndustriesQueryResponse>();
		}
	}
}
