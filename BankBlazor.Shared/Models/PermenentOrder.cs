using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBlazor.Shared.Models;

public partial class PermenentOrder
{
    [Key]
    public int OrderId { get; set; }

    [ForeignKey(nameof(Account))]
    public int AccountId { get; set; }

    public string BankTo { get; set; } = null!;

    public string AccountTo { get; set; } = null!;

    public decimal? Amount { get; set; }

    public string Symbol { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
