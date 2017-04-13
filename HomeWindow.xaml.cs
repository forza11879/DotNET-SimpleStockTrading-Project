using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TradingApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        Database db;

        public HomeWindow()
        {
            try
            {
                db = new Database();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opning database connection: " + e.Message);
            }
            InitializeComponent();
            refreshStockQuotesList();
        }

        private void refreshStockQuotesList()
        {

            List<StockQuotes> StockQuotesList = db.GetAllStockQuotes();
            lvStockQuotesList.ItemsSource = StockQuotesList;

        }

        private void lvStockQuotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvStockQuotesList.SelectedItem == null)
            {
                //if there is no selection dissable buttons Update and Add
                btnBuy.IsEnabled = false;
                btnSell.IsEnabled = false;
            }
            else
            {
                //if there is a selectoin enable buttons Update and Add
                btnBuy.IsEnabled = true;
                btnSell.IsEnabled = true;
                //if there is a selection populate text boxes and combo box with the properties of the objetc selected in data grid
                StockQuotes selectedStockQuotes = (StockQuotes)lvStockQuotesList.SelectedItem;

                lblSymbol.Content = selectedStockQuotes.Symbol;
                lblCompanyName.Content = selectedStockQuotes.Name;
                tbTradingPrice.Text = selectedStockQuotes.LastTrade + "";


            }
        }
    }



}
}
