using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			// Pimary key
			builder.HasKey(x => x.CompanyId);
			builder.Property(x => x.CompanyId)
				.UseIdentityColumn();


			// Fields
			builder.Property(x => x.CompanyName)
				   .HasMaxLength(100)
				   .IsRequired();

			builder.Property(x => x.WebsiteUrl)
				   .HasMaxLength(255)
				   .IsRequired(false);


			builder.Property(x => x.Slug)
				   .HasMaxLength(100)
				   .IsRequired(true);
			builder.HasIndex(x => x.Slug)
				   .IsUnique();

			builder.Property(x => x.Description)
				   .HasMaxLength(1000)
				   .IsRequired(false);

			builder.Property(x => x.ShortDescription)
				   .HasMaxLength(300)
				   .IsRequired(false);

			builder.Property(x => x.Industry)
				   .HasMaxLength(100)
				   .IsRequired(false);

			builder.Property(x => x.CompanySize)
				   .HasMaxLength(50)
				   .IsRequired(false);

			builder.Property(x => x.FoundedYear)
				   .IsRequired(false);

			builder.Property(x => x.WebsiteUrl)
				   .HasMaxLength(255)
				   .IsRequired(false);

			builder.Property(x => x.LinkedInUrl)
				   .HasMaxLength(255)
				   .IsRequired(false);

			builder.Property(x => x.TwitterUrl)
				   .HasMaxLength(255)
				   .IsRequired(false);

			builder.Property(x => x.Email)
				   .HasMaxLength(255)
				   .IsRequired();

			builder.Property(x => x.PhoneNumber)
				   .HasMaxLength(50)
				   .IsRequired(false);

			builder.Property(x => x.Fax)
				   .HasMaxLength(50)
				   .IsRequired(false);

			builder.Property(x => x.IsFeatured)
				   .HasDefaultValue(false)
				   .IsRequired();

			builder.Property(x => x.IsVerified)
				   .HasDefaultValue(false)
				   .IsRequired();

			builder.Property(x => x.Location)
				   .HasMaxLength(255)
				   .IsRequired(false);

			// Relations
			builder.HasOne(x => x.CreatedByUser)
				.WithMany(x => x.CreatedCompanies)
				.HasForeignKey(x => x.CreatedByUserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.ToTable("Companies");
		}
	}
}
