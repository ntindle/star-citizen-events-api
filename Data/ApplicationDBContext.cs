using SCEAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SCEAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Name = "Red Festival", Description = "Description 1", StartDate = new DateOnly(2023, 1, 20), EndDate = new DateOnly(2023, 2, 6) },
            new Event { Id = 2, Name = "Coramor", Description = "Description 2", StartDate = new DateOnly(2023, 2, 11), EndDate = new DateOnly(2023, 2, 15) }
        );
    }

}
