using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task Delete(int id);
        Task Update(T entity);
    }
}
