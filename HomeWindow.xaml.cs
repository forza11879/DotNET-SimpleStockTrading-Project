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
//using System.Windows.Documents;
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
            UpdatePortfolioInfo();
            //GetListOfHistoricalStockFromYahoo();
            btnBuy.IsEnabled = false;
            btnSell.IsEnabled = false;


        }


        private void RefreshStockList()
        {
            try
            {
                lvStockQuotesList.ItemsSource = Globals.db.GetAllStockPricesFromDatabase();
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Error showing record in a list: " + e.Message, "Confirmation", MessageBoxButton.OK);
            }

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
            // if exists it updates record
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

        private void GetListOfHistoricalStockFromYahoo()
        {

            List<QuotesHistory> quoteHistoryList = QuotesHistoryLoader.LoadQuotesHistory("AAPL", 1962);
            

            try
            {

                foreach (QuotesHistory stockHistory in quoteHistoryList)
                {

                    Globals.db.AddQuotesHistoryTable(stockHistory);
                }


            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
        }

        

        private void lvStockQuotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvStockQuotesList.SelectedItem == null)
            {
                //if there is no selection dissable buttons Update and Add
                btnBuy.IsEnabled = false;
            }

            else
            {
                btnBuy.IsEnabled = true;
            }

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            int Quantity;

            // this part is cheking if record already exists in database
            // if exists it updates record
            // if not it adds new record

            if (int.TryParse(tbQuantity.Text, out Quantity))
            {
                StockDb SelectedStock = (StockDb)lvStockQuotesList.SelectedItem;
                List<String> SymbolStringLIstOwnedByUser = new List<String>();


                if (SymbolStringLIstOwnedByUser.Contains(SelectedStock.Symbol, StringComparer.OrdinalIgnoreCase))
                {
                    //adds transaction record and updates cash in portfolio
                    Globals.db.AddBuyTransaction(Globals.SelectedPortfolio, SelectedStock, Quantity);

                    //adds stock into users portfolio
                    Globals.db.AddPortfolioStock(Globals.SelectedPortfolio, SelectedStock, Quantity);

                }
                else
                {
                    //adds transaction record and updates cash in portfolio
                    Globals.db.AddBuyTransaction(Globals.SelectedPortfolio, SelectedStock, Quantity);

                    //updates stock volume and average price in portfolio
                    //NOT IMPLEMENTED YET


                }






                tbQuantity.Text = "";
                UpdatePortfolioInfo();
                MessageBox.Show("Transaction completed", "Confirmation", MessageBoxButton.OK);







            }
            else
            {
                MessageBox.Show("Invalid Qty", "Confirmation", MessageBoxButton.OK);
            }







        }


        private void UpdatePortfolioInfo()
        {

            lbCash.Content = Globals.SelectedPortfolio.Cash;


        }



    }



}

