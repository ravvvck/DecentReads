
using DecentReads.Application.DTOs.Book;
using DecentReads.Domain.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHost.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        Task<int> CreateAsync(Book book);
        void DeleteAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        IEnumerable<Book> GetByIdRange(List<int> ids);
        void UpdateAsync(Book book);
        Task<List<Book>> SearchByTitleOrAuthorAsync(string searchPhrase);

    }

    
}
