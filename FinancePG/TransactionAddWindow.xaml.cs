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
using System.Windows.Shapes;

namespace FinancePG
{
    public partial class TransactionAddWindow : Window
    {
        public CreditCard card;
        public Transaction transaction;
        public TransactionAddWindow(Transaction transaction, CreditCard card)
        {
            InitializeComponent();
            if (transaction == null)
                this.DataContext = new Transaction{Category = Transaction.CategoryOfTransaction.Семья,
                    DateOfTransaction = DateTime.Today, SumIncome = 0, Type = Transaction.TypeOfTransaction.Expense,
                    WhatIncome = "Enter the name", CreditCard = card};
            else
            {
                this.DataContext = new Transaction{CreditCard = card, Category = transaction.Category, 
                    DateOfTransaction = transaction.DateOfTransaction, SumIncome = transaction.SumIncome, 
                    Type = transaction.Type, WhatIncome = transaction.WhatIncome};
            }
            foreach (var item in Enum.GetValues(typeof(Transaction.TypeOfTransaction)))
            {
                ComboBoxType.Items.Add(item);
            }
            foreach (var items in Enum.GetValues(typeof(Transaction.CategoryOfTransaction)))
            {
                ComboBoxCategory.Items.Add(items);
            }
            DatePickerTransaction.SelectedDate = DateTime.Now.Date;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (transaction == null)
            transaction = new Transaction();
            try
            {
                transaction.WhatIncome = TextblockWhatIncome.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrent Transaction's name", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            try
            {
                transaction.SumIncome = double.Parse(TextBlockSum.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect sum's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            try
            {
                DatePickerTransaction.SelectedDateFormat = DatePickerFormat.Short;
                transaction.DateOfTransaction = DateTime.Parse(DatePickerTransaction.Text);
                DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect data", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            transaction = (Transaction)(this.DataContext);
        }
    }
}
