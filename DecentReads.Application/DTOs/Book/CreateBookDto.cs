using DecentReads.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.DTOs.Book
{
    public class CreateBookDto 
    {
       
        public string Name { get; set; }
        
        public string AuthorFirstName { get; set; }
        
        public string AuthorLastName { get; set; }
      
        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}
