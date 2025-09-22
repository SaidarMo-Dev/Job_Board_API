namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		private void MapGetCompaniesSummaryQuery()
		{
			CreateMap<Data.Entities.Company, Core.Feutures.Companies.Queries.Results.GetCompaniesSummaryQueryResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
				;
		}
	}
}
