using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{

			builder.Property(x => x.Gendor)
				.HasConversion(
					x => x.ToString(),
					x => (GendorEnum)Enum.Parse(typeof(GendorEnum), x)
				)
				.IsRequired();

			builder.HasOne(x => x.Country)
					.WithMany(x => x.Users)
					.HasForeignKey(x => x.CountryId)
					.IsRequired();

		}
	}


}
