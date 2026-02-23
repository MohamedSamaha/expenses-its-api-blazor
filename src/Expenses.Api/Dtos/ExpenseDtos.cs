using System.ComponentModel.DataAnnotations;

namespace Expenses.Api.Dtos;

public class ExpenseCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [Required]
    [RegularExpression("EGP|USD|EUR", ErrorMessage = "Currency must be EGP, USD, or EUR")]
    public string Currency { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public DateTime OccurredOn { get; set; }
}

public class ExpenseUpdateDto : ExpenseCreateDto
{
    public byte[]? RowVersion { get; set; }
}

public class ExpenseReadDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public DateTime OccurredOn { get; set; }

    public string CreatedByUserId { get; set; } = string.Empty;

    public string? RowVersion { get; set; }
}