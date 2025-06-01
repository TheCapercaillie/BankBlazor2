using BankBlazor.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankBlazor.Shared.Models;

namespace BankBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.Accounts)
                .ToListAsync();

            return customers;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }
        [HttpGet("accounts/{id}/balance")]
        public async Task<ActionResult<decimal>> GetBalance(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            return account.Balance;
        }

        [HttpPost("accounts/{id}/deposit")]
        public async Task<ActionResult<Account>> Deposit(int id, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            if (amount <= 0)
                return BadRequest("Deposit amount must be positive");

            account.Balance += amount;

            var transaction = new Transaction
            {
                AccountId = id,
                Amount = amount,
                Type = "Deposit",
                Date = DateTime.UtcNow
            };
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            return Ok(account);
        }

        [HttpPost("accounts/{id}/withdraw")]
        public async Task<ActionResult<Account>> Withdraw(int id, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            if (amount <= 0)
                return BadRequest("Withdrawal amount must be positive");

            if (account.Balance < amount)
                return BadRequest("Insufficient funds");

            account.Balance -= amount;

            var transaction = new Transaction
            {
                AccountId = id,
                Amount = -amount,
                Type = "Withdrawal",
                Date = DateTime.UtcNow
            };
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            return Ok(account);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferRequest request)
        {
            var sourceAccount = await _context.Accounts.FindAsync(request.FromAccountId);
            var targetAccount = await _context.Accounts.FindAsync(request.ToAccountId);

            if (sourceAccount == null || targetAccount == null)
                return NotFound("One or both accounts not found");

            if (request.Amount <= 0)
                return BadRequest("Transfer amount must be positive");

            if (sourceAccount.Balance < request.Amount)
                return BadRequest("Insufficient funds");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                sourceAccount.Balance -= request.Amount;
                targetAccount.Balance += request.Amount;

                var withdrawalTx = new Transaction
                {
                    AccountId = request.FromAccountId,
                    Amount = -request.Amount,
                    Type = "Transfer Out",
                    Date = DateTime.UtcNow
                };
                var depositTx = new Transaction
                {
                    AccountId = request.ToAccountId,
                    Amount = request.Amount,
                    Type = "Transfer In",
                    Date = DateTime.UtcNow
                };

                _context.Transactions.AddRange(withdrawalTx, depositTx);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { Message = "Transfer successful" });
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Transfer failed");
            }
        }

        [HttpGet("accounts/{id}/transactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int id)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        [HttpPost("accounts")]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBalance), new { id = account.Id }, account);
        }

        [HttpGet("accounts/{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null)
                return NotFound();

            return account;
        }
    }

    public class TransferRequest
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
