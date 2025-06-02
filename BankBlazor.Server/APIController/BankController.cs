using BankBlazor.Server.Data;
using BankBlazor.Server.Models;
using BankBlazor.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly BankBlazorContext _context;

        public BankController(BankBlazorContext context)
        {
            _context = context;
        }

        [HttpGet("customers/{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) return NotFound();

            var dto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Accounts = customer.Accounts.Select(a => new AccountDto
                {
                    Id = a.Id,
                    Balance = a.Balance
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet("customers")]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(customers);
        }

        [HttpPost("accounts/{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
        {
            if (amount <= 0) return BadRequest("Invalid amount");

            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();

            account.Balance += amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = id,
                Amount = amount,
                Type = "Deposit",
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("accounts/{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
            if (amount <= 0) return BadRequest("Invalid amount");

            var account = await _context.Accounts.FindAsync(id);
            if (account == null || account.Balance < amount) return BadRequest("Insufficient funds");

            account.Balance -= amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = id,
                Amount = -amount,
                Type = "Withdrawal",
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            if (request.Amount <= 0) return BadRequest("Invalid amount");

            var from = await _context.Accounts.FindAsync(request.FromAccountId);
            var to = await _context.Accounts.FindAsync(request.ToAccountId);
            if (from == null || to == null || from.Balance < request.Amount) return BadRequest("Transfer error");

            from.Balance -= request.Amount;
            to.Balance += request.Amount;

            _context.Transactions.AddRange(new[]
            {
                new Transaction { AccountId = from.Id, Amount = -request.Amount, Type = "Transfer Out", Date = DateTime.UtcNow },
                new Transaction { AccountId = to.Id, Amount = request.Amount, Type = "Transfer In", Date = DateTime.UtcNow }
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("accounts/{id}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions(int id)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionDto
                {
                    Date = t.Date,
                    Type = t.Type,
                    Amount = t.Amount
                })
                .ToListAsync();

            return Ok(transactions);
        }
    }

    public class TransferRequest
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}

