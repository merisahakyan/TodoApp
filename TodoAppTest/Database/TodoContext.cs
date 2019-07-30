
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppTest.Database.FluentApi;

namespace TodoAppTest.Database
{
    public class TodoContext:DbContext
    {
        string connection = "";
        public TodoContext(string connection)
        {
            this.connection = connection;
        }

        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ListItemConfiguraion());
            modelBuilder.ApplyConfiguration(new TodoListConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApplicationDb;Trusted_Connection=True;ConnectRetryCount=0");
                optionsBuilder.UseSqlServer(connection);
        }
    }

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TodoContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApplicationDb;Trusted_Connection=True;ConnectRetryCount=0");
            return new TodoContext("Server=(localdb)\\mssqllocaldb;Database=ApplicationDb;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }
}
