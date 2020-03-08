using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NanoCell.WebUI.Areas.Identity;
//using NanoCell.WebUI.Data;
using NanoCell.Application;
using NanoCell.Infrastructure;
using NanoCell.Application.Common.Interfaces;
using NanoCell.UI;
using NanoCell.WebUI.Services;
using Blazored.Toast;
using Syncfusion.EJ2.Blazor;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace NanoCell.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment);
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            //services.AddBlazoredToast();
            services.AddUI();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins", builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //    });
            //});
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE2NTE5QDMxMzcyZTM0MmUzMEQ1amZaMTBuSHVnQWpFK2FDMjJpc21sQ3VhTi9NV2cxSlR2L24xcDhMdmM9;MjE2NTIwQDMxMzcyZTM0MmUzMEQ4TC92c0h2b2lSUU9BaVZ1VE1nR05DWkFiNVhOOGVpMFREMUU2bm42V1E9;MjE2NTIxQDMxMzcyZTM0MmUzMFkyNmdDejBnUTVmdUtTbDJKOTBjSUkyMGhTNTBQTXpwRTRZaUpOeXlDcVU9;MjE2NTIyQDMxMzcyZTM0MmUzMGd3aEs3dldvR2M1cmRNOW9oTndSNm5sOXNOdnY4YjBIaXVjWWpabUlJdEE9;MjE2NTIzQDMxMzcyZTM0MmUzMFJHK0ttNUxSdzdDaFNEbjFsdEVkYy82bHVkRVhCS1dCdFQxVlQxd0cyTkU9;MjE2NTI0QDMxMzcyZTM0MmUzMEJGdGtvd3E3bTArcXByc05qQWVwWTVyOFMwUnhwWWkzSUd3K2pGODFLSDg9;MjE2NTI1QDMxMzcyZTM0MmUzME85ZzJvSDd2R3RtYnZQT0ZCMUNKUTB3YWE5UUh6MUYxRU5mcU15NnRnUk09;MjE2NTI2QDMxMzcyZTM0MmUzMFB6YW9ZaDBvQmgxTTkyLzRJUG0ybkJOdWlRL3FzakpqRm0xa3BHbUs1Vk09;MjE2NTI3QDMxMzcyZTM0MmUzMEZkR0x2SXdrY2wzNXBvWHZjNlBtd0NibUNIWXlvUmZNN3IzTVBpV2w1UEU9;NT8mJyc2IWhiZH1nfWN9YGpoYmF8YGJ8ampqanNiYmlmamlmanMDHmgnOzo2PjcjfTY4NDogEzQ+Mjo/fTA8Pg==;MjE2NTI4QDMxMzcyZTM0MmUzMGoyd0w5QnZYV3RpTkZEMUppV1dsWldndnJGRU9BeE1HS2g1YzIyTEI4YW89");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
