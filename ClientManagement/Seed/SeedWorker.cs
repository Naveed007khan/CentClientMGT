using Centangle.ClientManager.Context;
using ClientManagement.Contexts;
using ClientManagement.Data;
using ClientManagement.Helpers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientManagement.Seed
{
    public class SeedWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedWorker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ClientIdentityManagerContext>();
            //await context.Database.EnsureCreatedAsync(cancellationToken);

            await context.Database.MigrateAsync();
            //await scope.ServiceProvider.GetRequiredService<ClientContext>().Database.MigrateAsync();

            var alreadyCreatedRoles = context.Roles.Any();
            if (!alreadyCreatedRoles)
            {
                var roleModel = new ApplicationRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = "5acd8273-22f2-487b-8971-f0208a532051",
                };

                context.Roles.Add(roleModel);
                context.SaveChanges();
            }
            var alreadyCreatedUsers = context.Users.Any();
            if (!alreadyCreatedUsers)
            {
                var userModel = new ApplicationUser
                {
                    UserName = "admin@centangle.com",
                    Email = "admin@centangle.com",
                    EmailConfirmed = true
                };
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var result = await userManager.CreateAsync(userModel, "29*ajpg@isb");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "SuperAdmin");
                }
            }


        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
