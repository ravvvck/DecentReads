using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Exceptions;
using DecentReads.Domain.Books;
using DecentReads.Domain.Books.Entities;
using DecentReads.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DecentReadsDbContext dbContext;

        public ReviewRepository(DecentReadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddAsync(int userId, int bookId, string content)
        {
            var validation = await dbContext.Reviews.Where(r => r.UserId == userId & r.BookId == bookId).FirstOrDefaultAsync();
            if (validation != null)
            {
                throw new BadRequestException("User has already reviewed this book.");
            }

            var newReview = Review.Create(content, bookId, userId);
            dbContext.Reviews.Add(newReview);
            await dbContext.SaveChangesAsync();
            return newReview.Id;


        }

        public async void DeleteAsync(int userId, int bookId)
        {
            var review = dbContext.Reviews.FirstOrDefault(r => r.UserId == userId & r.BookId == bookId);
            if (review == null) throw new NotFoundException("Review not found");
            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Review>> GetAllByBookIdAsync(int bookId)
        {
            var reviews = await dbContext.Reviews.AsNoTracking().Where(r => r.BookId == bookId).ToListAsync();
            return reviews;
        }

        public async Task<int> UpdateAsync(int userId, int bookId, string content)
        {
            var review = await dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId & r.BookId == bookId);
            if (review == null) throw new NotFoundException("Review not found");
            
            review.Content = content;
            review.LastModified= DateTime.UtcNow;
            dbContext.Reviews.Update(review);
            await dbContext.SaveChangesAsync();
            return review.Id;
        }
    }
}
