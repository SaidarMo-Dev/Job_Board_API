using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class ExperienceSkillConfiguration : IEntityTypeConfiguration<ExperienceSkill>
	{
		public void Configure(EntityTypeBuilder<ExperienceSkill> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Experience)
				.WithMany(e => e.Skills)
				.HasForeignKey(x => x.ExperienceId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(x => x.Skill)
				.WithMany(s => s.ExperienceSkills)
				.HasForeignKey(x => x.SkillId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(x => new { x.ExperienceId, x.SkillId })
				.IsUnique();
		}
	}
}
