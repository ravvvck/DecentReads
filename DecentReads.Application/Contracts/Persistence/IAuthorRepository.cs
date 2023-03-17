using DecentReads.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Contracts.Persistence
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
    }
}
