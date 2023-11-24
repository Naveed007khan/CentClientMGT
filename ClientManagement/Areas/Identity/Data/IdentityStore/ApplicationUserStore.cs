using ClientManagement.Helpers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientManagement.Data.IdentityStore
{
    public class ApplicationUserStore<TUser, TRole> : UserStore<TUser, TRole, ClientIdentityManagerContext, long>
        where TUser : ApplicationUser
        where TRole : ApplicationRole
    {
        private readonly ClientIdentityManagerContext db;
        public ApplicationUserStore(ClientIdentityManagerContext db) : base(db)
        {
            this.db = db;
        }
        public async override Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default)
        {

            bool combinationExists = await db.Users
            .AnyAsync(x => x.UserName == user.UserName
                        && x.Email == user.Email);
            if (combinationExists)
            {
                var IdentityError = new IdentityError { Description = "The specified username and email are already registered" };
                return IdentityResult.Failed(IdentityError);
            }
            return await base.CreateAsync(user);
        }
        public override Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default)
        {
            return base.AddLoginAsync(user, login, cancellationToken);
        }
        public override async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            // var tenantId = GetApplicationId();
            //  return await Users.Where(u => u.NormalizedEmail == normalizedEmail && u.ApplicationId == tenantId).FirstOrDefaultAsync();
            return await Users.Where(u => u.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync();
        }
        public override async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            //  var appId = GetApplicationId();
            //   var user = await Users.Where(u => u.NormalizedUserName == normalizedUserName && u.ApplicationId == appId).FirstOrDefaultAsync();
            return await Users.Where(u => u.NormalizedUserName == normalizedUserName).FirstOrDefaultAsync();
        }
    }
}
