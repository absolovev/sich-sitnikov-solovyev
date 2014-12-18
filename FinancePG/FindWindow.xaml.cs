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
        private CreditCard Card;
        public FindWindow(CreditCard card)
        {
            InitializeComponent();
            Card = card;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ListViewFindCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TransactionAddWindow transwindow = new TransactionAddWindow(manager.ListOfCards[ListViewFindCards.SelectedIndex]);
                transwindow.ShowDialog();
            }
            catch (Exception)
            {
                return;
            }
        }

    }
}
