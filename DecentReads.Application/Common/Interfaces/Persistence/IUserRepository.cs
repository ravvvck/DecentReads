

using DecentReads.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<DecentReads.Domain.Users.User> UpdateAsync(DecentReads.Domain.Users.User user);
        DecentReads.Domain.Users.User? GetUserByEmail(string email);
        DecentReads.Domain.Users.User? GetUserByUserId(int userId);
        Task<DecentReads.Domain.Users.User?> GetUserByUsernameAsync(string username);
        DecentReads.Domain.Users.User? GetUserByRefreshToken(string refreshToken);
        void Register(DecentReads.Domain.Users.User user);
        Task LogoutAsync(string refreshToken, int userId);
    }
}
