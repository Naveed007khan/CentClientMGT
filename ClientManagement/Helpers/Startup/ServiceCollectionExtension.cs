using Centangle.Common.ResponseHelpers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Centangle.ClientManager.Context;
using ClientManagement.Helpers.Models.Shared.Mapper;
using Centangle.Base.User.Identity;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Services;
using ClientManagement.Contexts;

namespace RoleManagement.Helpers.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static void RunMigrations<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            TContext service = services.BuildServiceProvider().GetService<TContext>();
            service.Database.Migrate();
        }
        public static void AddClientManagementDI<TIdentityKey, TClientKey>(this IServiceCollection services)
            where TClientKey : IEquatable<TClientKey>
            where TIdentityKey : IEquatable<TIdentityKey>
        {
            IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddDbContext<ClientContext>(options =>
            {
                options.UseSqlServer(
                configuration.GetConnectionString("ClientConnection")
                , b => b.MigrationsAssembly("ClientManagement")
                );
            });

            services.AddControllersWithViews();

            services.AddRazorPages();

            //services.AddAutoMapper(typeof(Mapping).Assembly);
            services.AddScoped<IRepositoryResponse, RepositoryResponse>();
            services.AddScoped(typeof(IUserIdentityService<,>), typeof(UserIdentityService<,>));
            services.AddScoped(typeof(IClient<,>), typeof(ClientService<,>));
            services.AddScoped(typeof(ITenant<,>), typeof(TenantService<,>));
            services.AddScoped(typeof(IBranch<,>), typeof(BranchService<,>));
            services.AddScoped(typeof(IModule<,>), typeof(ModuleService<,>));
            services.AddScoped(typeof(IModulePermission<,>), typeof(ModulePermissionService<,>));
        }
    }
}
