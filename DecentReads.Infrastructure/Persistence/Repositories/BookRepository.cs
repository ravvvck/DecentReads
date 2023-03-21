using DecentReads.Application.DTOs.Book;
using DecentReads.Application.Exceptions;
using DecentReads.Domain.Books;
using GameHost.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DecentReadsDbContext dbContext;

        public BookRepository(DecentReadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await dbContext.Books.Include(b => b.Author).ToListAsync();
            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await dbContext.Books.DefaultIfEmpty().Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) throw new NotFoundException("Book not found");

            
            return book;
        }
        public IEnumerable<Book> GetByIdRange(List<int> ids)
        {
            var books = dbContext.Books.DefaultIfEmpty().Include(b => b.Author).Where(b => ids.Contains(b.Id));
            return books;
        }

        public async Task<int> CreateAsync(Book book)
        {
       
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book.Id;
        }
        public async void DeleteAsync(int id)
        {
            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) throw new NotFoundException("Book not found");

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();

        }
        public async void UpdateAsync(Book book)
        {
            var existingBook = dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) throw new NotFoundException("Book not found");

            
            dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> SearchByTitleOrAuthorAsync(string searchPhrase)
        {
            var result = dbContext.Books.Include(b => b.Author)
                    .Where(x => (x.Name.ToLower() + x.Author.FullName.ToLower()).Contains(searchPhrase.ToLower())).DefaultIfEmpty().Take(5).ToListAsync();
            return result.Result;
        }
    }
}
