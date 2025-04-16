using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
	{
		public void Configure(EntityTypeBuilder<JobCategory> builder)
		{
			builder.HasKey(x => new { x.JobListingId, x.CategoryId });

			builder.HasOne(x => x.category)
				.WithMany(x => x.JobCategories)
				.HasForeignKey(x => x.CategoryId)
				.IsRequired();

			builder.HasOne(x => x.jobListing)
				.WithMany(x => x.jobCategories)
				.HasForeignKey(x => x.JobListingId)
				.IsRequired();

			builder.ToTable("JobCategories");

		}
	}

}
