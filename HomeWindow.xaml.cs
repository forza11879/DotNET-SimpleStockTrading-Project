using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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


        public HomeWindow()
        {
            InitializeComponent();
            Globals.db = new Database();
            GetListOfStocksFromYahoo();
            RefreshStockList();


        }


        private void RefreshStockList()
        {
            lvStockQuotesList.ItemsSource = Globals.db.GetAllStockPricesFromDatabase();
        }

        private void GetListOfStocksFromYahoo()
        {
            string csvData;

            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=AAPL+GOOG+MSFT&f=snbaopl1vhgkj");
            }


            //list of stocks from Yahoo
            List<YahooStock> ListOfStocksFromYahoo = YahooStock.Parse(csvData);


            //List of Stocks from database
            List<String> SymbolStringLIst = new List<String>();
            SymbolStringLIst = Globals.db.GetAllSymbolsFromDatabase();



            // this part is cheking if record already exists in database
            // if exists it updates recird
            // if not it adds new record
            try
            {
                foreach (YahooStock stock in ListOfStocksFromYahoo)
                {
                    try
                    {
                        if (SymbolStringLIst.Contains(stock.Symbol, StringComparer.OrdinalIgnoreCase))
                        {
                            Globals.db.UpdateStockToStockTable(stock);
                        }
                        else
                        {
                            Globals.db.AddStockToStockTable(stock);

                        }
                    }
                    catch (SqlException ext)
                    {
                        MessageBox.Show("Error adding/updating record: " + ext.Message, "Confirmation", MessageBoxButton.OK);
                        Console.Write(ext.StackTrace);
                    }

                }

            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }




        }






        /*  private void lvStockQuotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
          }*/
    }



}
//}
