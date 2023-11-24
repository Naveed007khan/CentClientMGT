using System;
using ClientManagement.Data;
using ClientManagement.Data.IdentityManager;
using ClientManagement.Data.IdentityStore;
using ClientManagement.Helpers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ClientManagement.Areas.Identity.IdentityHostingStartup))]
namespace ClientManagement.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ClientIdentityManagerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ClientManagementConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddRoles<IdentityRole>()
                //    .AddEntityFrameworkStores<ClientIdentityManagerContext>();

                services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireUppercase = false;
                })
                .AddUserStore<ApplicationUserStore<ApplicationUser, ApplicationRole>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<ClientIdentityManagerContext>()
                .AddSignInManager<ApplicationSignInManager<ApplicationUser>>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders()
                 .AddDefaultUI();


                //services.AddDefaultIdentity<PortalUser>()
                //    .AddRoles<IdentityRole>()
                //    .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}