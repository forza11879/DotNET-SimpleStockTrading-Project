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
        private Database db;

        public HomeWindow()
        {
            InitializeComponent();
            db = new Database();
            GetListOfStocksFromYahoo();
        }

       private void GetListOfStocksFromYahoo()
        {
            string csvData;

            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=AAPL+GOOG+MSFT&f=snbaopl1vhgkj");
            }

            List<Stock> ListOfStocksFromYahoo = Stock.Parse(csvData);

            try { foreach (Stock stock in ListOfStocksFromYahoo)
            {        
                     db.AddStockToStockTable(stock);
                    
                }
            }catch(NullReferenceException e)
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
