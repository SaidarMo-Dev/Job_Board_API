using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
	{
		public void Configure(EntityTypeBuilder<JobSkill> builder)
		{
			builder.HasKey(x => new { x.SkillId, x.JobListingId });

			builder.HasOne(x => x.skillInfo)
				.WithMany(x => x.jobSkills)
				.HasForeignKey(x => x.SkillId)
				.IsRequired();


			builder.HasOne(x => x.jobListing)
				.WithMany(x => x.JobSkills)
				.HasForeignKey(x => x.JobListingId)
				.IsRequired();

			builder.ToTable("JobsSkills");
		}

	}

}
