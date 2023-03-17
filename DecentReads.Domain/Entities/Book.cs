using DecentReads.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Entities
{
    public  class Book : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Author Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}
