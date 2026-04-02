using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserJobPreferenceConfiguration : IEntityTypeConfiguration<UserJobPreference>
	{
		public void Configure(EntityTypeBuilder<UserJobPreference> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithOne(u => u.JobPreference)
				.HasForeignKey<UserJobPreference>(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(x => x.UserId)
				.IsUnique();

			builder.Property(x => x.DesiredJobTitle)
				.HasMaxLength(150);

			builder.Property(x => x.PreferredLocation)
				.HasMaxLength(150);

			builder.Property(x => x.WorkType)
				.HasMaxLength(50);

			builder.Property(x => x.IsOpenToWork)
				.HasDefaultValue(false);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(x => x.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
