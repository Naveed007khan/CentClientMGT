using Centangle.Base.User.Identity;
using Centangle.ClientManager.Context;
using ClientManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientManagement.Contexts
{
    public class ClientContext : ClientDbContext<Guid, Guid>// USED JUST FOR MIGRATIONS
    {
        public ClientContext(DbContextOptions options) : base(options)
        {

        }

        public ClientContext(DbContextOptions options, IUserIdentityService<Guid, Guid> userIdentityService) : base(options, userIdentityService)
        {
            
        }

        protected override void ApplyGlobalFilters(ModelBuilder builder)
        {
        }
    }
}
