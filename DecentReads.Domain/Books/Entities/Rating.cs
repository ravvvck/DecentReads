using DecentReads.Domain.Common;
using DecentReads.Domain.Common.Models;
using DecentReads.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Books.Entities
{
    public sealed class Rating : BaseAuditableEntity
    {
        [RegularExpression(@"1|2|3|4|5")]
        public int Value { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public Rating(int value, int bookId, int userId, DateTime created)
        {
            Value = value;
            BookId = bookId;
            UserId = userId;
            Created = created;
        }

        public static Rating Create(int value, int bookId, int userId, DateTime created)
        {
            return new(value,bookId, userId, created);
        }

        private Rating()
        {

        }
        


    }
}
