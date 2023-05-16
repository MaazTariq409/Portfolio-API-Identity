

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Models;

namespace Portfolio_API.Data
{
    public class PorfolioContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
	{
        public PorfolioContext(DbContextOptions<PorfolioContext> options) : base(options) 
        { 
        }

        public DbSet<User> user { get; set; }
        public DbSet<About> about { get; set; }
        public DbSet<Resume> resume { get; set; }
        public DbSet<Skills> skills { get; set; }
        public DbSet<Education> educations { get; set; }
        public DbSet<UserProjects> userProjects { get; set; }
        public DbSet<UserExperience> userExperiences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
		}
	}
}
