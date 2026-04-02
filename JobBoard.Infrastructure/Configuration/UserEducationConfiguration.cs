using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserEducationConfiguration : IEntityTypeConfiguration<UserEducation>
	{
		public void Configure(EntityTypeBuilder<UserEducation> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Educations)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.SchoolName)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(x => x.Degree)
				.HasMaxLength(150);

			builder.Property(x => x.FieldOfStudy)
				.HasMaxLength(150);

			builder.Property(x => x.Description)
				.HasMaxLength(1000);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(x => x.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
