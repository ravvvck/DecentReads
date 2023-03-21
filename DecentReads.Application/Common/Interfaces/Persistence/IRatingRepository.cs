using DecentReads.Domain.Books.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Interfaces.Persistence
{
    public interface IRatingRepository
    {
        Task<int> AddOrUpdateAsync(int userId, int bookId, int value);
        void DeleteAsync(int id);
        Task<List<Rating>> GetAllAsync();
        Task<int> GetRatingForUserByBookIdAsync(int bookId, int userId);
        Task<List<Rating>> GetAllRatingsForUserByIdAsync(int userId);
    }
}
