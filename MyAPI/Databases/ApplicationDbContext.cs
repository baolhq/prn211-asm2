using Microsoft.EntityFrameworkCore;
using MyAPI.Models;

namespace MyAPI.Databases
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}