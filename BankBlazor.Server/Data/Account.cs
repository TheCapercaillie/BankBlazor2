using System.Collections.Generic;

namespace BankBlazor.Server.Data
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
