using Microsoft.AspNetCore.Identity;

namespace Data.Entites.Identity
{
    public class Users : IdentityUser<int>
    {
        public string Address { get; set; }
        public string Country { get; set; }

    }
}
