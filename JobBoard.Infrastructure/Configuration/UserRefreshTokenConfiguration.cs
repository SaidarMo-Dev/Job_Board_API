using JobBoard.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Configuration
{
	public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
	{
		public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).UseIdentityColumn();

			builder.HasOne(x => x.User)
				.WithMany(x => x.userRefreshTokens)
				.HasForeignKey(x => x.UserId);

			builder.ToTable("UserRefreshTokens");
		}
	}
}
