using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePG
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int CardId { get; set; }
        [ForeignKey("CardId")]
        public CreditCard Card { get; set; }
    }
}