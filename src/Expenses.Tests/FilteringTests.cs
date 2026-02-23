using Expenses.Api.Data;
using Expenses.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class FilteringTests
{
    [Fact]
    public async Task Should_Filter_By_Category()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("FilterDb")
            .Options;

        using var context = new AppDbContext(options);

        context.Expenses.AddRange(
            new Expense
            {
                Title = "Food 1",
                Amount = 50,
                Currency = "USD",
                Category = "Food",
                OccurredOn = DateTime.Today
            },
            new Expense
            {
                Title = "IT 1",
                Amount = 100,
                Currency = "USD",
                Category = "IT",
                OccurredOn = DateTime.Today
            });

        await context.SaveChangesAsync();

        var foodExpenses = await context.Expenses
            .Where(e => e.Category == "Food")
            .ToListAsync();

        Assert.Single(foodExpenses);
    }
}