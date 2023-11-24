using ClientManagement.Helpers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientManagement.Data.IdentityStore.ClaimHelpers
{
    public static class ClaimsHelper
    {
        public static async Task<List<Claim>> GetClaims(ApplicationUser user, ClientIdentityManagerContext db)
        {
            var roleIds = await db.UserRoles.Where(x => x.UserId == user.Id).Select(x=>x.RoleId.ToString()).ToListAsync();
            var email = !string.IsNullOrEmpty(user.Email) ? user.Email : "";
            var fullName = string.IsNullOrEmpty(user.NormalizedUserName) ? "" : user.NormalizedUserName;
            var claims = new List<Claim>();
            claims.Add(new Claim("RoleIds", String.Join(",", roleIds)));
            claims.Add(new Claim("Email", email.ToString()));
            claims.Add(new Claim("FullName", fullName.ToString()));
            return claims;
        }
    }
}
