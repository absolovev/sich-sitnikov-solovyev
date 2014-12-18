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
            //Context = context;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            creditCard = new CreditCard { Bank = BankTextBox.Text, Number = int.Parse(numberTextBox.Text), Currency = CurrencyTextBox.Text };

            owner = new Owner { Age = int.Parse(AgeTextBox.Text), Name = NameTextBox.Text, Surname = SurNameTextBox.Text, Card = creditCard };

            //Context.CreditCards.Add(creditCard);
            //Context.Owners.Add(owner);

            DialogResult = true;
        }
    }
}
