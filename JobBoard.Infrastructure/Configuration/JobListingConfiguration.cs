using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class JobListingConfiguration : IEntityTypeConfiguration<JobListing>
	{
		public void Configure(EntityTypeBuilder<JobListing> builder)
		{

			builder.HasQueryFilter(x => x.CreatedByUser.IsDeleted == false);

			builder.HasKey(x => x.JobId);
			builder.Property(x => x.JobId)
				.UseIdentityColumn();

			builder.Property(x => x.Title)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Description)
				.HasMaxLength(255);

			builder.Property(x => x.Location)
				.HasMaxLength(200);

			builder.Property(x => x.JobType)
				.HasConversion(
					x => x.ToString(),
					x => (JobTypeEnum)Enum.Parse(typeof(JobTypeEnum), x)
				)
				.IsRequired();


			builder.Property(x => x.ExperienceLevel)
				.HasConversion(
					x => x.ToString(),
					x => (ExperienceLevelEnum)Enum.Parse(typeof(ExperienceLevelEnum), x)
				)
				.IsRequired();

			builder.Property(x => x.Status)
				.HasConversion(
					x => x.ToString(),
					x => (JobStatusEnum)Enum.Parse(typeof(JobStatusEnum), x)
					)
				.IsRequired();

			builder.HasOne(x => x.company)
				.WithMany(x => x.JobsListing)
				.HasForeignKey(x => x.CompanyId)
				.IsRequired();

			builder.HasOne(x => x.CreatedByUser)
				.WithMany(x => x.CreatedJobs)
				.HasForeignKey(x => x.CreatedByUserId);

			builder.ToTable("JobsListings");

		}
	}
}
