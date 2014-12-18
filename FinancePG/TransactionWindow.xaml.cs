using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    /// <summary>
    /// Логика взаимодействия для TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        public Transaction transaction;
        public CreditCard Card;
        private double income, outcome, sum;
        private bool g;
        public TransactionWindow(CreditCard card, Owner owner, FinanceContext _financeContext)
        {
            Card = card;
            InitializeComponent();
            textBlockCreditCard.Text = card.Number.ToString();
            textBlockBank.Text = card.Bank;
            textBlockOwner.Text = owner.Name + " " + owner.Surname;
            UpdateData();
            g = true;
        }

        private void listViewTransactions_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToolTipsTransactions.IsEnabled = false;
            ToolTipsTransactions.IsOpen = false;
        }

       

        private void UpdateData()
        {
            listViewTransactions.Items.Clear();
            foreach (var card in Card.ListOfTransactions)
            {
                listViewTransactions.Items.Add(card);
            }
            SumCount(Card);
        }

        private void SumCount(CreditCard card)
        {
            income = 0;
            outcome = 0;
            try
            {
                for (int i = 0; i < card.ListOfTransactions.Count; i++)
                {
                    string str = card.ListOfTransactions.ToList()[i].Type.ToString();
                    if (str == "Expense")
                    {
                        outcome = outcome + card.ListOfTransactions.ToList()[i].SumIncome;
                    }

                    else
                    {
                        income = income + card.ListOfTransactions.ToList()[i].SumIncome;
                    }
                }
                sum = income - outcome;
                BalanceTextBlock.Text = sum.ToString();
            }
            catch (Exception)
            { MessageBox.Show("Error with calculating sum", "Error", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction();
        }

        private void Adding(object sender, RoutedEventArgs e)
        {
            AddTransaction();
            
        }
        private void AddTransaction()
        {
            TransactionAddWindow TransAddWindow = new TransactionAddWindow(transaction, Card);
            TransAddWindow.ShowDialog();
            if (TransAddWindow.DialogResult.HasValue && TransAddWindow.DialogResult.Value)
            {
                if (string.IsNullOrWhiteSpace(TransAddWindow.transaction.SumIncome.ToString()) ||
                    string.IsNullOrWhiteSpace(TransAddWindow.transaction.WhatIncome) ||
                    string.IsNullOrWhiteSpace(TransAddWindow.transaction.DateOfTransaction.ToString()))
                    return;
                Card.ListOfTransactions.Add(TransAddWindow.transaction);
                UpdateData();
            }
        }
        private void listViewTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ToolTipEnabling();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ToolTipEnabling();
        }
        private void ToolTipEnabling()
        {
            try
            {
                WhatIncomeToolTip.Text = "Transaction's name:    " +
                                         Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex].WhatIncome;
                CategoryToolTip.Text = "Category of the Transaction:  " +
                                       Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex].Category
                                           .ToString();
                SumIncomeTolltip.Text = "Transaction's sum: " +
                                        Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex].SumIncome
                                            .ToString();
                TypeTooltip.Text = "Type of Transaction:   " +
                                   Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex].Type.ToString();
                DateToolTip.Text = "Date of the transaction:  " +
                                   Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex]
                                       .DateOfTransaction.ToString();
                ToolTipsTransactions.IsEnabled = true;
                ToolTipsTransactions.IsOpen = true;
            }
            catch { }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
           var transAddWindow = new TransactionAddWindow
               (Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex], Card);
           if (transAddWindow.DialogResult.HasValue && transAddWindow.DialogResult.Value)
            {
                    if (string.IsNullOrWhiteSpace(transAddWindow.transaction.SumIncome.ToString()) ||
                        string.IsNullOrWhiteSpace(transAddWindow.transaction.WhatIncome) ||
                        string.IsNullOrWhiteSpace(transAddWindow.transaction.DateOfTransaction.ToString()))
                        return;
                Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex] = transAddWindow.transaction;
                    UpdateData();
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            transaction = Card.ListOfTransactions.ToList()[listViewTransactions.SelectedIndex];
            Card.ListOfTransactions.Remove(transaction);
            UpdateData();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Card.ListOfTransactions.Clear();
            UpdateData();

    }
}
