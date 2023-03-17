using DecentReads.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Entities
{
    public class Author :BaseDomainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
