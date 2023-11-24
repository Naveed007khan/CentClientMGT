using Microsoft.AspNetCore.Identity;

namespace ClientManagement.Helpers.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
    }
    public class ApplicationRole : IdentityRole<long>
    {
    }
}
