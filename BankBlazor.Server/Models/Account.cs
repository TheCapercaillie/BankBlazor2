using System;
using System.Collections.Generic;

namespace BankBlazor.Server.Models;

public partial class Account
{
    public int Id { get; set; }

    public decimal Balance { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
