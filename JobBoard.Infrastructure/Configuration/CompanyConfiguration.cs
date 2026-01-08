using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasKey(x => x.CompanyId);
			builder.Property(x => x.CompanyId)
				.UseIdentityColumn();



			builder.Property(x => x.CompanyName)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Description)
				.HasMaxLength(255)
				.IsRequired(false);

			builder.Property(x => x.WebsiteUrl)
				.HasMaxLength(255)
				.IsRequired(false);

			builder.Property(x => x.Location)
				.HasMaxLength(255)
				.IsRequired();

			builder.HasOne(x => x.CreatedByUser)
				.WithMany(x => x.CreatedCompanies)
				.HasForeignKey(x => x.CreatedByUserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(x => x.LogoFile)
				.WithMany()
				.HasForeignKey(x => x.LogoFileId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.ToTable("Companies");
		}
	}
}
