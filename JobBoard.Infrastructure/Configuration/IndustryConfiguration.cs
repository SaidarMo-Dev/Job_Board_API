using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{

	public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
	{
		public void Configure(EntityTypeBuilder<Industry> builder)
		{
			builder.ToTable("Industries");

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(i => i.Slug)
				.IsRequired()
				.HasMaxLength(160);

			builder.HasIndex(i => i.Slug)
				.IsUnique();

			builder.Property(i => i.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}

}
