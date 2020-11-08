using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using websocketschat.Core.Models;

namespace websocketschat.Core.Repositories
{
    /// <summary>
    /// Defines User repository interface.
    /// </summary>
    public interface IUserRepository
    {
        Task AddUserAsync(User coreUser);
        Task<User> GetUserAsync(string username);
        Task<User> GetUserWithRoleAsync(string username);
        Task<bool> UserExistsAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();
    }
}
