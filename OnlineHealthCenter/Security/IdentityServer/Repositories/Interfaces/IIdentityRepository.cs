using IdentityServer.Entities;

namespace IdentityServer.Repositories.Interfaces
{
    public interface IIdentityRepository
    {
        Task<bool> CreateUser(User user, string password);
        Task<bool> AddRoleToUser(User user, string role);
    }
}
