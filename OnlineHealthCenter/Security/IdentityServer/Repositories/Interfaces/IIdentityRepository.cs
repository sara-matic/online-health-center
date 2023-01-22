using IdentityServer.Entities;

namespace IdentityServer.Repositories.Interfaces
{
    public interface IIdentityRepository
    {
        Task<bool> CreateUser(User user, string password);
        Task<bool> AddRoleToUser(User user, string role);
        Task<User?> GetUserByUsername(string username);
        Task<bool> CheckUserPassword(User user, string password);
        Task<IEnumerable<string>> GetUserRoles(User user);
    }
}
