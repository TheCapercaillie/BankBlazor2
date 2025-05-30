using System;

namespace BankBlazor.Server.Data
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }

        // Navigation property
        public Account Account { get; set; }
    }
}
