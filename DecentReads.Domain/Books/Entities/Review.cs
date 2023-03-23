using DecentReads.Domain.Common;
using DecentReads.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Books.Entities
{
    public sealed class Review : BaseAuditableEntity
    {
        

        public string Content { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


        private Review(string content, int bookId, int userId)
        {
            Content = content;
            BookId = bookId;
            UserId = userId;
            Created = DateTime.Now;
        }

        public static Review Create(string content, int bookId, int userId)
        {
            return new(content, bookId, userId);
        }

        private Review()
        {

        }


    }
}
