using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecentReads.Domain.Users;
using DecentReads.Domain.Users.ValueObjects;

namespace GameHost.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUserTable(builder);
        }

       

        private void ConfigureUserTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);

            builder.OwnsMany(m => m.FavoriteBooks, sb =>
            {
                sb.ToTable("FavoriteBooks");
                sb.WithOwner().HasForeignKey("UserId");
                sb.HasKey("Id");



            });

            

        }

        

    }
}
