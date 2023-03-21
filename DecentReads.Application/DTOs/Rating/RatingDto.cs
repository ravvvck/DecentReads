using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.DTOs.Rating
{
    public class RatingDto
    {
        public int User_id { get; set; }
        public int Rating { get; set; }
        public int Book_id { get; set; }
    }
}
