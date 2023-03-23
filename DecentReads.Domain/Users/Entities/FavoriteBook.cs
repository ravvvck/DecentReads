using DecentReads.Domain.Books;
using DecentReads.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Users.Entities
{
    public class FavoriteBook : BaseEntity
    {
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime Created { get; set; }

        private FavoriteBook(int bookId, DateTime created)
        {
            
            BookId = bookId;
            Created = created;
        }

        public static FavoriteBook Create(int bookId, DateTime created)
        {
            return new(bookId, created);
        }
    }
}
