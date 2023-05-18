using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Repositories.Interfaces
{
    public interface IIdentityRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<bool> AddRoleToUser(User user, string role);
        Task<User?> GetUserByUsername(string username);
        Task<bool> CheckUserPassword(User user, string password);
        Task<IEnumerable<string>> GetUserRoles(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task AddRefreshTokenToDb(RefreshToken refreshToken);
        Task AddRefreshTokenToUser(User user, RefreshToken refreshToken);
        Task RemoveRefreshTokenFromUser(User user, string refreshToken);
        Task RemoveRefreshTokenFromDb(string refreshToken);
        Task DeleteUser(User user);
        Task<bool> ChangePassword(User user, string oldPassword, string newPassword);
    }
}
