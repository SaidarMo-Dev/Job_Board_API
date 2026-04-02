using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserCertificationConfiguration : IEntityTypeConfiguration<UserCertification>
	{
		public void Configure(EntityTypeBuilder<UserCertification> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Certifications)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.IssuingOrganization)
				.HasMaxLength(150);

			builder.Property(x => x.CredentialId)
				.HasMaxLength(150);

			builder.Property(x => x.CredentialUrl)
				.HasMaxLength(500);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(x => x.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
