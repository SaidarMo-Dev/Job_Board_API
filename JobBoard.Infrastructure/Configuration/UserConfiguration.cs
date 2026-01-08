using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasQueryFilter(x => x.IsDeleted == false);

			builder.Property(x => x.Gender)
				.HasConversion(
					x => x.ToString(),
					x => (GendorEnum)Enum.Parse(typeof(GendorEnum), x)
				)
				.IsRequired(false);

			builder.HasOne(x => x.Country)
					.WithMany(x => x.Users)
					.HasForeignKey(x => x.CountryId)
					.IsRequired(false);

			builder.Property(x => x.Code)
				.HasConversion(new EncryptionConverter());


			builder
			.HasOne(x => x.ProfileImageFile)
			.WithMany()
			.HasForeignKey(x => x.ProfileImageFileId)
			.OnDelete(DeleteBehavior.Restrict);


		}
	}


}
