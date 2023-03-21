using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Application.Exceptions;
using DecentReads.Domain.Authors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DecentReadsDbContext dbContext;

        public AuthorRepository(DecentReadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Author> AddAsync(string fullName)
        {
            if (fullName == null) throw new BadRequestException("Author's name cannot be empty.");
            var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.FullName == fullName);
            if (author != null)
            {
                return author;
            }
            var newAuthor = Author.Create(fullName);
            dbContext.Authors.Add(newAuthor);
            await dbContext.SaveChangesAsync();
            return newAuthor;

        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> GetByFullNameAsync(string fullName)
        {
            var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.FullName == fullName);
            return author;
        }

        public void UpdateAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
