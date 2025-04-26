using JobBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class BookMarkConfiguration : IEntityTypeConfiguration<Bookmark>
	{
		public void Configure(EntityTypeBuilder<Bookmark> builder)
		{

			builder.HasQueryFilter(x => x.userInfo.IsDeleted == false);

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
