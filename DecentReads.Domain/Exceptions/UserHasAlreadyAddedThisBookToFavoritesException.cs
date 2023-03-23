using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Exceptions
{
    public class UserHasAlreadyAddedThisBookToFavoritesException : DomainException
    {
        public UserHasAlreadyAddedThisBookToFavoritesException(string message) : base(message)
        { }
    }
}
