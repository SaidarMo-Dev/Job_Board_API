using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void AddApplicationMapping()
		{
			CreateMap<AddApplicationCommand, Application>()
				.ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(x => x.Status, opt => opt.MapFrom(src => ApplicationStatusEnum.Pending))
				.ForMember(x => x.LastStatusDate, opt => opt.MapFrom(src => DateTime.UtcNow));

		}


	}
}
