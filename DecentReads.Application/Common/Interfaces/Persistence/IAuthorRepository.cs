using DecentReads.Domain.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Interfaces.Persistence
{
    public interface IAuthorRepository
    {
        Task<Author> AddAsync(string fullName);
        void DeleteAsync(int id);
        void UpdateAsync(Author author);
        Task<Author?> GetByFullNameAsync(string fullName);
    }
}
