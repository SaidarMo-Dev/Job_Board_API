using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(x => x.CategoryId);
			builder.Property(x => x.CategoryId)
				.UseIdentityColumn();

			builder.Property(x => x.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Description)
				.HasMaxLength(255)
				.IsRequired(false);


			builder.ToTable("Categories");

		}
	}

}
