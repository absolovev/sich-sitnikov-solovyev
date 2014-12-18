using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancePG
{
    public class FinanceContext : DbContext
    {
        public FinanceContext() : base("HomeFinancialDB") { }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}
