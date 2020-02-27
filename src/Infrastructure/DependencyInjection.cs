using NanoCell.Application;
using NanoCell.Application.Common.Interfaces;
//using NanoCell.Infrastructure.Files;
using NanoCell.Infrastructure.Identity;
using NanoCell.Infrastructure.Persistence;
using NanoCell.Infrastructure.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NanoCell.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "NanoCell.Cookie";
                config.LoginPath = "/Identity/Account/Login";
            });
            if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "NanoCell.IntegrationTests",
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedScopes = { "NanoCell.WebUIAPI", "openid", "profile" }
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "jason@clean-architecture",
                            Password = "NanoCell!",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "jason@clean-architecture")
                            }
                        }
                    });
            }
            else
             {
            //    services.AddIdentityServer( )
            //        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>( );

                services.AddTransient<IDateTime, DateTimeService>();
                services.AddTransient<IIdentityService, IdentityService>();
              //  services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            }

            //services.AddAuthentication("OAuth")
            //    .AddJwtBearer("OAuth", config =>
            //    {
            //        var sb = Encoding.UTF8.GetBytes("aaaaaaaaaaa");
            //        var key = new SymmetricSecurityKey(sb);
            //        config.Events = new JwtBearerEvents()
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                if (context.Request.Query.ContainsKey("access_token"))
            //                {

            //                }
            //            }
            //        };
            //        config.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = 
            //        }
            //    });

            return services;
        }
    }
}
