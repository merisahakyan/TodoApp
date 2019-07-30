using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Database.FluentApi
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Title)
                .IsRequired();

            builder
                .HasOne<User>(l => l.User)
                .WithMany(u => u.TodoLists)
                .HasForeignKey(uw => uw.UserId)
                .IsRequired();
        }
    }
}
