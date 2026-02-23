using Microsoft.EntityFrameworkCore;
using Expenses.Domain;

namespace Expenses.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .Property(e => e.RowVersion)
            .IsRowVersion();
    }
}