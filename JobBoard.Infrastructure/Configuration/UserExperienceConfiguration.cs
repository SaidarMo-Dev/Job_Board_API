using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
	{
		public void Configure(EntityTypeBuilder<UserExperience> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Experiences)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.JobTitle)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(x => x.CompanyName)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(x => x.EmploymentType)
				.HasMaxLength(50);

			builder.Property(x => x.Location)
				.HasMaxLength(150);

			builder.Property(x => x.Description)
				.HasMaxLength(2000);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(x => x.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
