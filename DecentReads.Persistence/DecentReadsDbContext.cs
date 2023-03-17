using DecentReads.Domain.Common;
using DecentReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Persistence
{
    public class DecentReadsDbContext : DbContext
    {
        public DecentReadsDbContext(DbContextOptions<DecentReadsDbContext> options) : base(options) 
        {

        }




        public DbSet<Book>  Books { get; set; }
        public DbSet<Author> Authors { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DecentReadsDbContext).Assembly);
        }

        
    }
}
