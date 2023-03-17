using DecentReads.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.DTOs.Author
{
    public class AuthorDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
