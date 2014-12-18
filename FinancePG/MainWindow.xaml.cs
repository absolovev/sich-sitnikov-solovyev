using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
            _financeContext.CreditCards.Load();
            _financeContext.Owners.Load();
            _financeContext.Transactions.Load();
            UpdateData();
        }


        public void UpdateData()
        {
            listViewCards.Items.Clear();
            if (_financeContext.CreditCards.ToList().Count == 0)
                MessageBox.Show("Your list of card is empty!");
            else
            {
                foreach (var card in _financeContext.CreditCards.ToList())
                {
                    listViewCards.Items.Add(card);
                }
            }
           
        }

        private void listViewCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var transactionWindow = new TransactionWindow(_financeContext.CreditCards.ToList()[listViewCards.SelectedIndex],
                _financeContext.Owners.ToList()[listViewCards.SelectedIndex], _financeContext);
                transactionWindow.ShowDialog();
                var card = _financeContext.CreditCards.ToList()[listViewCards.SelectedIndex];
                card = transactionWindow.Card;
                _financeContext.Entry(card).State = EntityState.Modified;
                _financeContext.SaveChanges();
                UpdateData();
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private void listViewCards_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToolTipsCreditCard.IsEnabled = false;
            ToolTipsCreditCard.IsOpen = false;
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                AgeToolTip.Text = "Owner's Age:   " + _financeContext.Owners.ToArray()[listViewCards.SelectedIndex].Age.ToString();
                NameToolTip.Text = "Owner's Name:   " + _financeContext.Owners.ToArray()[listViewCards.SelectedIndex].Name;
                SurnameToolTip.Text = "Owner's SurName:   " + _financeContext.Owners.ToArray()[listViewCards.SelectedIndex].Surname;
                ToolTipsCreditCard.IsEnabled = true;
                ToolTipsCreditCard.IsOpen = true;
            }
            catch
            {
                
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var AddCC = new CreditCardAddWindow();
            AddCC.ShowDialog();
            if (AddCC.DialogResult.HasValue && AddCC.DialogResult.Value)
            {
                _financeContext.CreditCards.Add(AddCC.creditCard);
                _financeContext.Owners.Add(AddCC.owner);
                _financeContext.SaveChanges();
            };
            UpdateData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var creditCard = new CreditCardAddWindow(_financeContext.CreditCards
                .ToList()[listViewCards.SelectedIndex], _financeContext.Owners.ToList()[listViewCards.SelectedIndex]);
            creditCard.ShowDialog();
            CreditCard card = _financeContext.CreditCards
                .ToList()[listViewCards.SelectedIndex];
            Owner owner = _financeContext.Owners.ToList()[listViewCards.SelectedIndex];
            if (creditCard.DialogResult.HasValue && creditCard.DialogResult.Value)
            {
                card.Bank = creditCard.creditCard.Bank;
                card.Currency = creditCard.creditCard.Currency;
                card.Number = creditCard.creditCard.Number;
                owner.Name = creditCard.owner.Name;
                owner.Surname = creditCard.owner.Surname;
                owner.Age = creditCard.owner.Age;
            }
            if (card == null) return;
            _financeContext.Entry(card).State = EntityState.Modified;
            if (owner != null)
                _financeContext.Entry(owner).State = EntityState.Modified;
            _financeContext.SaveChanges();
            UpdateData();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            CreditCard cardToDelete = _financeContext.CreditCards.ToList()[listViewCards.SelectedIndex];
            Owner ownerToDelete = _financeContext.Owners.ToList()[listViewCards.SelectedIndex];
            _financeContext.CreditCards.Remove(cardToDelete);
            _financeContext.Owners.Remove(ownerToDelete);
            _financeContext.SaveChanges();
            UpdateData();
        }

       

    }
}
