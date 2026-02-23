using Expenses.Api.Data;
using Expenses.Api.Dtos;
using Expenses.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
    int page = 1,
    int pageSize = 10,
    string? category = null,
    DateTime? from = null,
    DateTime? to = null)
    {
        var query = _context.Expenses.AsQueryable();

        if (!string.IsNullOrEmpty(category))
            query = query.Where(e => e.Category == category);

        if (from.HasValue)
            query = query.Where(e => e.OccurredOn >= from.Value);

        if (to.HasValue)
            query = query.Where(e => e.OccurredOn <= to.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.OccurredOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            totalCount,
            page,
            pageSize,
            items
        });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseReadDto>> GetById(int id)
    {
        var e = await _context.Expenses.FindAsync(id);

        if (e == null)
            return NotFound();

        return Ok(new ExpenseReadDto
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Currency = e.Currency,
            Category = e.Category,
            OccurredOn = e.OccurredOn,
            CreatedByUserId = e.CreatedByUserId,
            RowVersion = e.RowVersion != null
                ? Convert.ToBase64String(e.RowVersion)
                : null
        });
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(ExpenseCreateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var expense = new Expense
        {
            Title = dto.Title,
            Amount = dto.Amount,
            Currency = dto.Currency,
            Category = dto.Category,
            OccurredOn = dto.OccurredOn,
            CreatedByUserId = userId!
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
    }


    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExpenseUpdateDto dto)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            return NotFound();

        expense.Title = dto.Title;
        expense.Amount = dto.Amount;
        expense.Currency = dto.Currency;
        expense.Category = dto.Category;
        expense.OccurredOn = dto.OccurredOn;

        _context.Entry(expense).Property(e => e.RowVersion).OriginalValue = dto.RowVersion;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("This record was modified by another user.");
        }

        return NoContent();
    }


    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
            return NotFound();

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}