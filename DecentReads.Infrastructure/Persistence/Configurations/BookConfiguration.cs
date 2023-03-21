using DecentReads.Domain.Books;
using DecentReads.Domain.Books.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameHost.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {

            ConfigureBookTable(builder);
        }

        private void ConfigureBookTable(EntityTypeBuilder<Book> builder)
        {
            builder.Ignore(p => p.Ratings);
        }

        


    }
}
