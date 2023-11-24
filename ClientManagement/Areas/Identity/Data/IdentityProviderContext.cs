using ClientManagement.Helpers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Data
{
    
    public class ClientIdentityManagerContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public ClientIdentityManagerContext(DbContextOptions<ClientIdentityManagerContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
