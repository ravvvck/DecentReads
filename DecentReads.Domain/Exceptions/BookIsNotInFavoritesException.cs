using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Exceptions
{
    internal class BookIsNotInFavoritesException : DomainException
    {
        public BookIsNotInFavoritesException(string message) : base(message)
        { }
    }
}
