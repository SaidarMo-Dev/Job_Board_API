using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
	{
		public void Configure(EntityTypeBuilder<UserLanguage> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Languages)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.Language)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Proficiency)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(x => x.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
