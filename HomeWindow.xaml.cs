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
            
            GetListOfStocksFromYahoo();
            RefreshStockList();
            UpdateUserBalance();
            UpdatePortfolioInfo();
            RefreshStockOwnedByPortfolio();
            //GetListOfHistoricalStockFromYahoo();
            btnBuy.IsEnabled = false;
            btnSell.IsEnabled = false;


        }


        private void RefreshStockList()
        {
            try
            {
                lvStockQuotesList.ItemsSource = Globals.Db.GetAllStockPricesFromDatabase();
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Error showing record in a list: " + e.Message, "Confirmation", MessageBoxButton.OK);
                Console.Write(e.StackTrace);
            }

        }

        private void RefreshStockOwnedByPortfolio()
        {
            try
            {
                lvStockOwnedByUser.ItemsSource = Model.DBA_PortfolioStock.GetAll(Globals.SelectedPortfolio);
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
                csvData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=AAPL+GOOG+MSFT+ADBE+AKAM+ALXN+AMZN+AAL+AMGN&f=snbaopl1vhgkj");
            }


            //list of stocks from Yahoo
            List<YahooStock> ListOfStocksFromYahoo = YahooStock.Parse(csvData);


            //List of Stocks from database
            List<String> SymbolStringLIst = new List<String>();
            SymbolStringLIst = Globals.Db.GetAllSymbolsFromDatabase();



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
                            Globals.Db.UpdateStockToStockTable(stock);
                        }
                        else
                        {
                            Globals.Db.AddStockToStockTable(stock);

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

            

            List<Entities.QuotesHistory> quoteHistoryList = Entities.QuotesHistoryLoader.LoadQuotesHistory("RRR", 1962);
            

            try
            {

                foreach (Entities.QuotesHistory stockHistory in quoteHistoryList)
                {

                    Globals.Db.AddQuotesHistoryTable(stockHistory);
                }


            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
        }

        

        private void lvStockQuotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Entities.StockDb SelectedStock = (Entities.StockDb)lvStockQuotesList.SelectedItem;
            lblCompanyName.Content = SelectedStock.Symbol;
            lbBid.Content = SelectedStock.Bid;
            lbAsk.Content = SelectedStock.Ask;

            if (lvStockQuotesList.SelectedItem == null)
            {
                //if there is no selection dissable buttons Update and Add
                btnBuy.IsEnabled = false;
            }

            else
            {
                btnBuy.IsEnabled = true;
                btnSell.IsEnabled = false;
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
                Entities.StockDb SelectedStock = (Entities.StockDb)lvStockQuotesList.SelectedItem;
                List<String> SymbolStringLIstOwnedByUser = new List<String>();
                SymbolStringLIstOwnedByUser = Globals.Db.GetAllStockOwnedByUser(Globals.SelectedPortfolio);

                if (SymbolStringLIstOwnedByUser.Contains(SelectedStock.Symbol, StringComparer.OrdinalIgnoreCase))
                {
                    //adds transaction record and updates cash in portfolio
                    Globals.Db.AddBuyTransaction(Globals.SelectedPortfolio, SelectedStock, Quantity);

                    //adds stock into users portfolio
                    Globals.Db.UpdatePortfolioStock(Globals.SelectedPortfolio, SelectedStock, Quantity);

                }
                else
                {
                    //adds transaction record and updates cash in portfolio
                    Globals.Db.AddBuyTransaction(Globals.SelectedPortfolio, SelectedStock, Quantity);

                    //updates stock volume and average price in portfolio
                    Globals.Db.AddPortfolioStock(Globals.SelectedPortfolio, SelectedStock, Quantity);


                }


                tbQuantity.Text = "";
                
                MessageBox.Show("Transaction completed", "Confirmation", MessageBoxButton.OK);


            }
            else
            {
                MessageBox.Show("Invalid Qty", "Confirmation", MessageBoxButton.OK);
            }


            RefreshStockOwnedByPortfolio();
            UpdateUserBalance();
            UpdatePortfolioInfo();


        }


        private void UpdatePortfolioInfo()
        {

            Entities.Portfolio updatedPortfolio = Model.DBA_Portfolio.GetUpdatedPortfolio(Globals.SelectedPortfolio);

            lbCash.Content = updatedPortfolio.Cash;
            lbGanesLooses.Content = updatedPortfolio.Balance;

        }

        private void UpdateUserBalance()
        {

            decimal totalSum = 0;
            decimal newBalance =0;

            Entities.Portfolio updatedPortfolio = Model.DBA_Portfolio.GetUpdatedPortfolio(Globals.SelectedPortfolio);
            decimal currentCash = updatedPortfolio.Cash;

            List<Entities.PortfolioStock> PortfolioList = Model.DBA_PortfolioStock.GetAll(Globals.SelectedPortfolio);


            List<Entities.StockDb> DatabaseStock = Globals.Db.GetAllStockPricesFromDatabase();

            for (int i = 0; i < DatabaseStock.Count; ++i)
            {
                var tempSymbol = DatabaseStock.ElementAt(i).Symbol;
                foreach (Entities.PortfolioStock E in PortfolioList)
                {
                    if (E.Symbol == tempSymbol)
                    {
                        totalSum += (decimal)(E.SharesOwned * DatabaseStock.ElementAt(i).Bid);
                    }
                }
            }


            newBalance = (currentCash + totalSum) - 50000;

            Model.DBA_Portfolio.UpdateBalance(newBalance, Globals.SelectedPortfolio);

        }


        private void lvStockOwnedByUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvStockQuotesList.SelectedItem == null)
            {
                //if there is no selection dissable buttons Update and Add
                btnSell.IsEnabled = false;
            }

            else
            {
                btnSell.IsEnabled = true;
                btnBuy.IsEnabled = false;
            }

        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {

            int Quantity;

            // this part is cheking if record already exists in database
            // if exists it updates record
            // if not it adds new record

            if (int.TryParse(tbQuantity.Text, out Quantity))
            {
                Entities.PortfolioStock SelectedStockOwnedByUSer = (Entities.PortfolioStock)lvStockOwnedByUser.SelectedItem;
                List <Entities.StockDb> DatabasePrices= Globals.Db.GetAllStockPricesFromDatabase();


             //   Entities.StockDb stockItem = DatabasePrices.Find(Entities.StockDb);
                   



                tbQuantity.Text = "";
                UpdatePortfolioInfo();
                MessageBox.Show("Transaction completed", "Confirmation", MessageBoxButton.OK);


            }
            else
            {
                MessageBox.Show("Invalid Qty", "Confirmation", MessageBoxButton.OK);
            }



        }





    }



}

