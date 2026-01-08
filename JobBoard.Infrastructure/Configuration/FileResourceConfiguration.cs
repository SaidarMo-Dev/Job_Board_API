using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class FileResourceConfiguration
	: IEntityTypeConfiguration<FileResource>
	{
		public void Configure(EntityTypeBuilder<FileResource> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.UseIdentityColumn();

			builder.Property(x => x.Path).IsRequired();
			builder.Property(x => x.Bucket).IsRequired();

			builder.HasIndex(x => x.Path).IsUnique();

			builder.ToTable("FileResources");
		}
	}

}
