using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Portfolio_API.Controllers;
using Portfolio_API.Data;
using Portfolio_API.Models;
using Portfolio_API.Repository;
using Portfolio_API.Repository.Repository_Interface;
using System.Text;
using System.Text.Json.Serialization;

namespace Portfolio_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<PorfolioContext>(option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddScoped<IUserProfile, UserProfileRepository>();
            builder.Services.AddScoped<ISkills, SkillsRepository>();
            builder.Services.AddScoped<IEducation, EducationRepository>();
            builder.Services.AddScoped<IProjects, ProjectsRepository>();
            builder.Services.AddScoped<IUserExperience, UserExperienceRepository>();
            builder.Services.AddScoped<IInstitute, InstituteRepository>();
            builder.Services.AddScoped<ICity, CityRepository>();
            builder.Services.AddTransient<ICountry, CountryRepository>();
            builder.Services.AddTransient<IUserBlogs, UserBlogRepository>();


            builder.Services.AddTransient<TokenGeneration>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			// Add Identity
			builder.Services.AddIdentity<IdentityManual, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<PorfolioContext>()
				.AddDefaultTokenProviders();

			// Configure the scope for IdentityUser
			builder.Services.AddScoped<IdentityUser>();
			builder.Services.AddScoped<IdentityRole>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["authentication:Audiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

			app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapControllers();

            app.Run();
        }
    }
}