﻿

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Models;

namespace Portfolio_API.Data
{
    public class PorfolioContext : IdentityDbContext<IdentityManual>
	{
        public PorfolioContext(DbContextOptions<PorfolioContext> options) : base(options) 
        { 
        }

        
       // public DbSet<User> user { get; set; }

		public DbSet<IdentityManual> identityManuals { get; set; }
		public DbSet<UserProfile> userProfiles { get; set; }

		//public DbSet<About> about { get; set; }
        public DbSet<Resume> resume { get; set; }
        public DbSet<Skills> skills { get; set; }
        public DbSet<Education> educations { get; set; }
        public DbSet<UserProjects> userProjects { get; set; }
        public DbSet<UserExperience> userExperiences { get; set; }
        public DbSet<UserCity> userCity { get; set; }
        public DbSet<UserCountry> userCountry { get; set; }
        public DbSet<UserInstitute> userInstitute { get; set; }
        public DbSet<UserProducts> userProducts { get; set; }
        public DbSet<UserBlogs> userBlogs { get; set; }
        public DbSet<UserProductType> productTypes { get; set; }
        public DbSet<UserServices> userServices { get; set; }
        public DbSet<UserServiceGig> userServiceGigs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
		}
	}
}
