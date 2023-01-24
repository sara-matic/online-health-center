using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
