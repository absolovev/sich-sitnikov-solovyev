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
    /// <summary>
    /// Логика взаимодействия для CreditCardAddWindow.xaml
    /// </summary>
    public partial class CreditCardAddWindow : Window
    {
        //public FinanceContext Context;
        public Owner owner;
        public CreditCard creditCard;

        public CreditCardAddWindow()
        {
            InitializeComponent();
            
        }

        public CreditCardAddWindow(CreditCard card, Owner owner)
        {
            InitializeComponent();
            try
            {
                MessageBox.Show(card.Bank.ToString());
                BankTextBox.Text = card.Bank;
                numberTextBox.Text = card.Number.ToString();
                CurrencyTextBox.Text = card.Currency;
                NameTextBox.Text = owner.Name;
                SurNameTextBox.Text = owner.Surname;
                AgeTextBox.Text = owner.Age.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error with input data");
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            creditCard = new CreditCard();
            try
            {
                creditCard.Bank = BankTextBox.Text;
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect Bank's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                creditCard.Number = long.Parse(numberTextBox.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect Number's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                creditCard.Currency = CurrencyTextBox.Text;
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect Currency's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            owner = new Owner();
            try
            {
                owner.Age = int.Parse(AgeTextBox.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect Age's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                owner.Name = NameTextBox.Text;
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect FirstName's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                owner.Surname = SurNameTextBox.Text;
            }
            catch (Exception)
            {

                MessageBox.Show("Incorrect SurName's value", "Enter it again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            owner.Card = creditCard;
            DialogResult = true;
        }
    }
}
