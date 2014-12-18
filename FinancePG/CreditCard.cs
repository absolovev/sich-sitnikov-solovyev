using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancePG
{
    public class CreditCard
    {
        //[Key, ForeignKey("Owner")]
        public int Id { get; set; }
        public long Number { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }

        //Owner owner { get; set; }
        //public int OwnerId { get; set; }
        public ICollection<Transaction> ListOfTransactions { get; set; }

        public CreditCard()
        {
            ListOfTransactions = new List<Transaction>();
        }

    }
}
