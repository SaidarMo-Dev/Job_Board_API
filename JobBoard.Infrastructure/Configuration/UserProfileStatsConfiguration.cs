using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserProfileStatsConfiguration : IEntityTypeConfiguration<UserProfileStats>
	{
		public void Configure(EntityTypeBuilder<UserProfileStats> builder)
		{
			builder.HasKey(x => x.UserId);

			builder.HasOne(x => x.User)
				.WithOne(u => u.ProfileStats)
				.HasForeignKey<UserProfileStats>(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.ProfileCompletion)
				.HasDefaultValue(0);

			builder.Property(x => x.ProfileViews)
				.HasDefaultValue(0);
		}
	}
}
