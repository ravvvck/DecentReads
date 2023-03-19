

using DecentReads.Domain.Users;
using DecentReads.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
