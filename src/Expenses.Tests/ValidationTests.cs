using System.ComponentModel.DataAnnotations;
using Expenses.Domain; // adjust if needed
using Xunit;

public class ValidationTests
{
    [Fact]
    public void Amount_Should_Be_Greater_Than_Zero()
    {
        var expense = new Expense
        {
            Title = "Test",
            Amount = 0,
            Currency = "USD",
            Category = "Food",
            OccurredOn = DateTime.Today
        };

        var context = new ValidationContext(expense);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(expense, context, results, true);

        Assert.False(isValid);
    }
}