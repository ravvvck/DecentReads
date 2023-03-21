using DecentReads.Domain.Books;
using DecentReads.Domain.Common;
using DecentReads.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Authors
{
    public sealed class Author : BaseEntity
    {

        public string FullName { get; private set; }

        internal Author(string fullName)
        {
            FullName = fullName;
        }

        public static Author Create(string fullName)
        {
            return new(fullName);
        }

        private Author()
        {

        }
    }
}

