using IdentityServer.Data;
using IdentityServer.Entities;
using IdentityServer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IdentityContext context;

        public IdentityRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IdentityContext context)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
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

        public async Task<User?> GetUserByUsername(string username)
        {
            return await this.userManager.FindByNameAsync(username);
        }

        public async Task<bool> CheckUserPassword(User user, string password)
        {
            return await this.userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            return await this.userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await this.userManager.Users.ToListAsync();
        }

        public async Task AddRefreshTokenToDb(RefreshToken refreshToken)
        {
            this.context.RefreshTokens.Add(refreshToken);
            await this.context.SaveChangesAsync();
        }

        public async Task AddRefreshTokenToUser(User user, RefreshToken refreshToken)
        {
            user.RefreshTokens.Add(refreshToken);
            await this.userManager.UpdateAsync(user);
        }

        public async Task RemoveRefreshTokenFromUser(User user, string refreshToken)
        {
            user.RefreshTokens.RemoveAll(r => r.Token == refreshToken);
            await this.userManager.UpdateAsync(user);
        }

        public async Task RemoveRefreshTokenFromDb(string refreshToken)
        {
            var token = this.context.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);
            if (token == null)
            {
                return;
            }

            this.context.RefreshTokens.Remove(token);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            await this.userManager.DeleteAsync(user);
        }
    }
}
