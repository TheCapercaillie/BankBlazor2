using System;
using System.Collections.Generic;

namespace BankBlazor.Server.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public string Type { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Account Account { get; set; } = null!;
}
