using JobBoard.Data.Entities;
using JobBoard.Data.Helpers.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class PersonConfiguration : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.HasKey(x => x.PersonId);

			builder.Property(x => x.PersonId)
			.UseIdentityColumn();

			builder.Property(x => x.FirstName)
				.HasMaxLength(100)
				.IsRequired();


			builder.Property(x => x.LastName)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Gendor)
				.HasConversion(
					x => x.ToString(),
					x => (GendorEnum)Enum.Parse(typeof(GendorEnum), x)
				);

			builder.Property(x => x.Email)
				.HasMaxLength(255)
				.IsRequired();


			builder.Property(x => x.PhoneNumber)
				.HasMaxLength(100)
				.IsRequired(false);


			builder.Property(x => x.Address)
				.HasMaxLength(255)
				.IsRequired();

			builder.HasOne(x => x.CountryInfo)
				.WithMany(x => x.people)
				.HasForeignKey(x => x.CountryId)
				.IsRequired();

			builder.ToTable("People");
		}
	}

}
