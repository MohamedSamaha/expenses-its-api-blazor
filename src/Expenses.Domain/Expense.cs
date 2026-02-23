using System.ComponentModel.DataAnnotations;

namespace Expenses.Domain;
public class Expense
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]      
    public decimal Amount { get; set; }

    [Required]
    [RegularExpression("EGP|USD|EUR")]
    public string Currency { get; set; } = "";

    public string Category { get; set; } = string.Empty;

    public DateTime OccurredOn { get; set; }

    public string CreatedByUserId { get; set; } = string.Empty;
    public byte[]? RowVersion { get; set; }
}