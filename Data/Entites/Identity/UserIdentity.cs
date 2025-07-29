using Microsoft.AspNetCore.Identity;

namespace Data.Entites.Identity
{
    public class UserIdentity : IdentityUser<int>
    {
        public string FullName { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }
    }
}
