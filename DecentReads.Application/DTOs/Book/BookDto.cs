using DecentReads.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.DTOs.Book
{
    public class BookDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
        public string Genres { get; set; }
        public string Image_URL { get; set; }
        public string ISBN { get; set; }
        public int Publication_day { get; set; }
        public int Publication_month { get; set; }
        public int Publication_year { get; set; }
    }
}
