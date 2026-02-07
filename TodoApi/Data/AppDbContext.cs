using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
     : base(options)
        {
        }
        //  public DbSet<Todo> Todo { get; set; }
        public DbSet<Todo> Todo => Set<Todo>();
    }
}
