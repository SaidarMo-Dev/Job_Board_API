using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class CountryConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.HasKey(x => x.CountryId);

			builder.Property(x => x.CountryId)
				.ValueGeneratedNever();

			builder.Property(x => x.CountryName)
				.HasMaxLength(100)
				.IsRequired();

			builder.ToTable("Countries");

		}
	}

}
