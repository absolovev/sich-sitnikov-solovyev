using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancePG
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FinanceContext _financeContext = new FinanceContext();
        public MainWindow()
        {
            InitializeComponent();
            //GenerateData();
            //UpdateWindow();
        }

        public void GenerateData()
        {
            _financeContext.SaveChanges();
            listViewCards.DataContext = _financeContext;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreditCardAddWindow AddCC = new CreditCardAddWindow();
            AddCC.ShowDialog();
            if (AddCC.DialogResult.HasValue && AddCC.DialogResult.Value)
            {
                _financeContext.CreditCards.Add(AddCC.creditCard);
                _financeContext.SaveChanges();
                _financeContext.Owners.Add(AddCC.owner);
                _financeContext.SaveChanges();

                Transaction transaction = new Transaction
                {
                    Category = Transaction.CategoryOfTransaction.
                        Другое,
                    DateOfTransaction = new DateTime(1995, 1, 1),
                    SumIncome = 1111,
                    Type = Transaction.TypeOfTransaction.Получение,
                    WhatIncome = "s",
                    CreditCard = AddCC.creditCard
                };
                _financeContext.Transactions.Add(transaction);
                _financeContext.SaveChanges();
            };

        }
    }
}
