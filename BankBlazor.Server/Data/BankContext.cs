using Microsoft.EntityFrameworkCore;
using BankBlazor.Shared.Models;

namespace BankBlazor.Server.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Disposition> Dispositions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<PermenentOrder> PermenentOrders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermenentOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasOne(d => d.Account)
                    .WithMany()
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

           
        }

    }


    
}
