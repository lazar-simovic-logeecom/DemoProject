using DemoProject.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("User");
        
        modelBuilder.Entity<Category>()
            .ToTable("Category")
            .HasMany(c => c.SubCategories)
            .WithOne()
            .HasForeignKey(c => c.ParentCategory)
            .OnDelete(DeleteBehavior.Restrict);
    }
}