using Expenses.Api.Data;
using Expenses.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PaginationTests
{
    [Fact]
    public async Task Should_Return_Only_PageSize_Items()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("PaginationDb")
            .Options;

        using var context = new AppDbContext(options);

        context.Expenses.AddRange(
            Enumerable.Range(1, 20)
            .Select(i => new Expense
            {
                Title = $"Expense {i}",
                Amount = 10,
                Currency = "USD",
                Category = "Food",
                OccurredOn = DateTime.Today
            }));

        await context.SaveChangesAsync();

        var pageSize = 10;

        var result = await context.Expenses
            .OrderBy(e => e.Id)
            .Skip(0)
            .Take(pageSize)
            .ToListAsync();

        Assert.Equal(10, result.Count);
    }
}