
using DecentReads.Domain.Authors;
using DecentReads.Domain.Books.Entities;
using DecentReads.Domain.Books;
using DecentReads.Domain.Users.Enums;
using DecentReads.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Infrastructure.Persistence
{
    public class DecentReadsDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Rating> Ratings { get; set; }
        public DecentReadsDbContext(DbContextOptions<DecentReadsDbContext> options) : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DecentReadsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
