using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBlazor.Shared.Models.DTOs
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
