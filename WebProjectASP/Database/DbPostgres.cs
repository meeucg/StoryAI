using Microsoft.EntityFrameworkCore;
using WebProjectASP.Domain.Entities;

namespace WebProjectASP.Database;

public class DbPostgres(DbContextOptions<DbPostgres> options) : DbContext(options)
{
    public DbSet<Scene> Scenes { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>()
        //     .HasKey()
        
        base.OnModelCreating(modelBuilder);
    }
}