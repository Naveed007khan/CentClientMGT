using ClientManagement.Data.IdentityStore.ClaimHelpers;
using ClientManagement.Helpers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientManagement.Data.IdentityStore
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly ClientIdentityManagerContext _db;
        public ApplicationUserClaimsPrincipalFactory(
                                                    UserManager<ApplicationUser> userManager,
                                                    RoleManager<ApplicationRole> roleManager,
                                                    IOptions<IdentityOptions> optionsAccessor,
                                                    ClientIdentityManagerContext db
                                                    )
                                                    : base(userManager, roleManager, optionsAccessor)
        {
            _db = db;
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            //identity.AddClaim(new Claim("UserFirstName", user.FirstName ?? ""));
            //identity.AddClaim(new Claim("UserLastName", user.LastName ?? ""));
            identity.AddClaims(await ClaimsHelper.GetClaims(user, _db));
            return identity;
        }
      
    }
}
