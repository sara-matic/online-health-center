using IdentityServer.Data;
using IdentityServer.Entities;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<bool> CreateUser(User user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> AddRoleToUser(User user, string role)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (roleExists)
            {
                var result = await this.userManager.AddToRoleAsync(user, role);
                return result.Succeeded;
            }

            return false;
        }
    }
}
