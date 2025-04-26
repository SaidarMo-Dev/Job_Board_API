using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class SkillConfiguration : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.HasKey(x => x.SkillId);

			builder.Property(x => x.SkillId)
				.UseIdentityColumn();

			builder.Property(x => x.Name)
				.HasMaxLength(55)
				.IsRequired();

			builder.Property(x => x.Description)
					.HasMaxLength(255)
					.IsRequired(false);

			builder.ToTable("Skills");

		}
	}

}
