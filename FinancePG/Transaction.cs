using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancePG
{
    public class Transaction
    {
        public int Id { get; set; }
        public string WhatIncome { get; set; }
        public double SumIncome { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public enum TypeOfTransaction
        {
            Expense,
            Income,
        }

        public enum CategoryOfTransaction
        {
            ЖКХ,
            Связь,
            Еда,
            Отдых,
            Семья,
            Другое,
        }

        public TypeOfTransaction Type { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
