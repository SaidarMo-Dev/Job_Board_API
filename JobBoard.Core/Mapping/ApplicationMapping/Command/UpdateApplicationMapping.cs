using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void UpdateApplicationMapping()
		{
			CreateMap<UpdateApplicationCommand, Application>()
				.ForMember(dst => dst.LastStatusDate, opt => opt.MapFrom(src => DateTime.UtcNow));
		}
	}
}
