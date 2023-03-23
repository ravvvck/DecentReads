using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Exceptions;
using DecentReads.Domain.Books.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Persistence.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DecentReadsDbContext dbContext;

        public RatingRepository(DecentReadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Rating>> GetAllAsync()
        {
            var ratings = await dbContext.Ratings.DefaultIfEmpty().ToListAsync();
            return ratings;
        }



        public async Task<int> AddOrUpdateAsync(int userId, int bookId, int value)
        {

            var validation = dbContext.Ratings.FromSqlRaw($"SELECT * FROM [Ratings] WHERE UserId = {userId} and BookId = {bookId}").FirstOrDefault();


            if (validation != null)
            {
                validation.Value = value;

                dbContext.SaveChanges();
                return validation.Id;


            }
            var newRating = Rating.Create(value, bookId, userId, DateTime.Now);
            


            await dbContext.Ratings.AddAsync(newRating);
            await dbContext.SaveChangesAsync();
            return newRating.Id;



        }
        public async Task<int> GetRatingForUserByBookIdAsync(int bookId, int userId)
        {
            var validation = await dbContext.Ratings.FromSqlRaw($"SELECT * FROM [Ratings] WHERE UserId = {userId} and BookId = {bookId}")
                .Select(r => r.Value).FirstOrDefaultAsync();

            if (validation == null)
            {
                return 0;
            }
            return validation;
        }

        public async Task<List<Rating>> GetAllRatingsForUserByIdAsync(int userId)
        {
            var ratings = await dbContext.Ratings.AsNoTracking().Where(r => r.UserId == userId).ToListAsync();
            return ratings;
        }



        public async void DeleteAsync(int userId, int bookId)
        {
            var rating = dbContext.Ratings.FirstOrDefault(r => r.UserId == userId & r.BookId == bookId);
            if (rating == null) throw new NotFoundException("Rating not found");

            dbContext.Ratings.Remove(rating);
            await dbContext.SaveChangesAsync();
        }

       

    }
}
