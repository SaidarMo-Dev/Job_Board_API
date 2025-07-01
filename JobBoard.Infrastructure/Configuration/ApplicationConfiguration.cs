using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
	{
		public void Configure(EntityTypeBuilder<Application> builder)
		{
			builder.HasQueryFilter(x => x.UserInfo.IsDeleted == false);

			builder.HasKey(x => x.ApplicationId);

			builder.Property(x => x.ApplicationId)
				.UseIdentityColumn();


			builder.HasOne(x => x.JobListing)
				.WithMany(x => x.applications)
				.HasForeignKey(x => x.JobListingId)
				.IsRequired().OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(x => x.UserInfo)
				.WithMany(x => x.applications)
				.HasForeignKey(x => x.UserId)
				.IsRequired();

			builder.Property(x => x.status)
				.HasConversion(
					x => x.ToString(),
					x => (ApplicationStatusEnum)Enum.Parse(typeof(ApplicationStatusEnum), x)
					);

			builder.ToTable("Applications");
		}
	}
}
