using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        private List<CreditCard> Cards; 
        private bool G;
        private FinanceContext _financeContext;
        private Owner owner;
        public FindWindow(List<CreditCard> cards, bool g, FinanceContext financeContext)
        {
            _financeContext = financeContext;
            G = g; 
            Cards = cards;
            InitializeComponent();
            if (G == false)
                TextBlockFind.Text = "Enter Card's Bank";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (G == true)
            {
                UpdateFindList();
            }
            if (G == false)
            {
                UpdateFindList1();
            }
        }
        private void UpdateFindList()
        {
            List<CreditCard> findCard = new List<CreditCard>();
            findCard = Cards.FindAll(t => t.Number.ToString().StartsWith(TextBoxFind.Text));
            ListViewFindCards.Items.Clear();
            foreach (var card in findCard)
            {
                ListViewFindCards.Items.Add(card);
            }
        }
        private void UpdateFindList1()
        {
            List<CreditCard> findCard = new List<CreditCard>();
            findCard = Cards.FindAll(t => t.Bank.StartsWith(TextBoxFind.Text));
            ListViewFindCards.Items.Clear();
            foreach (var card in findCard)
            {
                ListViewFindCards.Items.Add(card);
            }
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
                ListViewFindCards.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ListViewFindCards.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void ListViewFindCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var query = _financeContext.Owners.ToList().Where(t=>t.Id == Cards[ListViewFindCards.SelectedIndex].Id).OrderByDescending(t=>t.Age);
            owner = query.ToList()[0];
            
            try
            {
                TransactionWindow transwindow = new TransactionWindow(Cards[ListViewFindCards.SelectedIndex], owner, _financeContext );
                transwindow.ShowDialog();
            }
            catch (Exception)
            {
                return;
            }
        }

    }
}
