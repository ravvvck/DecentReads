
using DecentReads.Domain.Users;
using DecentReads.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Contracts.Authentication
{
    public class AuthenticationResult
    {
        
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public AuthenticationResult(User user, string token, RefreshToken refreshToken)
        {
            
            Token = token;
            RefreshToken = refreshToken;
        }

        public AuthenticationResult(User user, string token)
        {
            
            Token = token;
        }
    }
    


}
