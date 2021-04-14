using Microsoft.EntityFrameworkCore;
using Pograminha.Domain.Model;
using System;

namespace Pograminha.Infra.SQL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
