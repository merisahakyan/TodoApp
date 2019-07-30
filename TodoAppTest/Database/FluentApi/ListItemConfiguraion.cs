using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Database.FluentApi
{
    public class ListItemConfiguraion : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Open)
                .IsRequired();

            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .HasOne<TodoList>(l => l.List)
                .WithMany(u => u.ListItems)
                .HasForeignKey(uw => uw.ListId)
                .IsRequired();
        }
    }
}
