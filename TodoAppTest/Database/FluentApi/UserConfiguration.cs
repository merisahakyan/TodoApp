using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Database.FluentApi
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
               .Property(b => b.Email)
               .IsRequired();

            builder
               .Property(b => b.Password)
               .IsRequired();

            builder
              .HasIndex(u => u.Email)
              .IsUnique();
        }
    }
}
