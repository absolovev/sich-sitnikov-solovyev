﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace FinancePG
{
    public partial class MainWindow : Window
    {
        private bool g;
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
                foreach (var card in _financeContext.CreditCards.ToList())
                {
                    listViewCards.Items.Add(card);
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
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            foreach (var entity in _financeContext.CreditCards)
                _financeContext.CreditCards.Remove(entity);
            _financeContext.SaveChanges();
            UpdateData();
        }
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                listViewCards.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            listViewCards.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

    }
}
