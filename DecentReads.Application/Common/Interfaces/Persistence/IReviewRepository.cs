using DecentReads.Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Interfaces.Persistence
{
    public interface IReviewRepository
    {
        Task<int> AddAsync(int userId, int bookId, string content);
        Task<int> UpdateAsync(int userId, int bookId, string content);
        Task DeleteAsync(int userId, int bookId);
        Task<List<Review>> GetAllByBookIdAsync(int bookId);
    }
}
