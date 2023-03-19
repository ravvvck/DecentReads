﻿

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
        Task<User> UpdateAsync(User user);
        User? GetUserByEmail(string email);
        User? GetUserByUserId(int userId);
        User? GetUserByRefreshToken(string refreshToken);
        void Register(User user);

    }
}