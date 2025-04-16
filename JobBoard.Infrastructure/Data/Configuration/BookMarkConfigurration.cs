using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Data.Configuration
{
	public class BookMarkConfiguration : IEntityTypeConfiguration<BookMark>
	{
		public void Configure(EntityTypeBuilder<BookMark> builder)
		{
			builder.HasKey(x => x.BookMarkId);
			builder.Property(x => x.BookMarkId)
				.UseIdentityColumn();

			builder.HasOne(x => x.jobListing)
				.WithMany(x => x.bookMarks)
				.HasForeignKey(x => x.JobId);


			builder.HasOne(x => x.userInfo)
				.WithMany(x => x.bookmarks)
				.HasForeignKey(x => x.UserId);

			builder.ToTable("BookMarks");

		}
	}
}
