using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{

	public class CompanyIndustryConfiguration : IEntityTypeConfiguration<CompanyIndustry>
	{
		public void Configure(EntityTypeBuilder<CompanyIndustry> builder)
		{
			builder.ToTable("CompanyIndustries");

			builder.HasKey(ci => new { ci.CompanyId, ci.IndustryId });

			builder.HasIndex(ci => ci.CompanyId);
			builder.HasIndex(ci => ci.IndustryId);

			builder.HasOne(ci => ci.Company)
				.WithMany(c => c.CompanyIndustries)
				.HasForeignKey(ci => ci.CompanyId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(ci => ci.Industry)
				.WithMany(i => i.CompanyIndustries)
				.HasForeignKey(ci => ci.IndustryId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
