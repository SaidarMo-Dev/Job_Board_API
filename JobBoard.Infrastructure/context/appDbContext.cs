using JobBoard.Data.Entities;
using JobBoard.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.context
{
	public class appDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{

		public appDbContext(DbContextOptions<appDbContext> options) : base(options)
		{
		}

		public DbSet<Country> countries { get; set; }
		public DbSet<Application> applications { get; set; }
		public DbSet<JobListing> jobs { get; set; }
		public DbSet<Company> companies { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<JobCategory> jobCategories { get; set; }
		public DbSet<Skill> skills { get; set; }
		public DbSet<JobSkill> jobSkills { get; set; }
		public DbSet<Bookmark> bookMarks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(appDbContext).Assembly);


		}
	}
}
