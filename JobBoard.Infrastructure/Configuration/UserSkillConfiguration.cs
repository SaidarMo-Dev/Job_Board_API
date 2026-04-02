using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
	{
		public void Configure(EntityTypeBuilder<UserSkill> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Skills)
				.HasForeignKey(x => x.UserId);

			builder.HasOne(x => x.Skill)
				.WithMany(s => s.UserSkills)
				.HasForeignKey(x => x.SkillId);

			builder.Property(x => x.Level)
				.HasMaxLength(50);
		}
	}
}
