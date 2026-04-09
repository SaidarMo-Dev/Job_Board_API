using JobBoard.Core.Common.Helpers;
using JobBoard.Core.Feutures.Companies.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CompanyMapping
{
	public partial class CompanyProfile
	{
		public void AddMappingForUpdateCompany()
		{
			CreateMap<UpdateCompanyCommand, Company>()
					.ForMember(dest => dest.CompanySize, opt => opt.MapFrom(src => CompanySizeHelper.GetSize(src.CompanySize)))
					.ForMember(dest => dest.Slug, opt => opt.MapFrom(src =>
					SlugHelper.Normalize(string.IsNullOrWhiteSpace(src.Slug) ? src.CompanyName : src.Slug)));
		}
	}
}
