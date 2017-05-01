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
            try
            {
            InitializeComponent();
            GetListOfStocksFromYahoo();
            RefreshStockList();
            UpdateUserBalance();
            UpdatePortfolioInfo();
            RefreshStockOwnedByPortfolio();           
            RefreshTransactions();
            }
            catch(SqlException e)
            {
                MessageBox.Show("SQL ERROR: " + e.Message, "Confirmation", MessageBoxButton.OK);
            }


            
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

        

        private void lvStockQuotesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Entities.StockDb SelectedStock = (Entities.StockDb)lvStockQuotesList.SelectedItem;

            string symbol = SelectedStock.Symbol;
            try
            {
            List<Entities.QuotesHistory> QuotesHistoryList = Entities.QuotesHistoryLoader.LoadQuotesHistory(symbol);
            FirstChartControl.DataSource = QuotesHistoryList;
            chartControl.DataSource = QuotesHistoryList;
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Error showing Graph: " + ex.Message, "Confirmation", MessageBoxButton.OK);
            }

        }

        






        private void GetListOfStocksFromYahoo()
        {
            string csvData;

            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=AAPL+GOOG+MSFT+ADBE+AMGN+ADBE+CMCSA+CSX+INTC+KHC+NVDA+SBUX+AMDA+AOBC+EA+CERN+CERS+ETFC+EBAY&f=snbaopl1vhgkj");
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
                Entities.StockDb SelectedStock = (Entities.StockDb)lvStockQuotesList.SelectedItem;

                lblCompanyNameBuyOrder.Content = SelectedStock.Symbol;
                lbBidBuyOrder.Content = SelectedStock.Bid;
                lbAskBuyOrder.Content = SelectedStock.Ask;


            }

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            int Quantity;

            // this part is cheking if record already exists in database
            // if exists it updates record
            // if not it adds new record

            if (int.TryParse(tbQuantityBuy.Text, out Quantity))
            {


                Entities.StockDb SelectedStock = (Entities.StockDb)lvStockQuotesList.SelectedItem;
                Entities.Portfolio SelectedPortfolio = Model.DBA_Portfolio.GetUpdatedPortfolio(Globals.SelectedPortfolio);
                List<String> SymbolStringLIstOwnedByUser = new List<String>();
                SymbolStringLIstOwnedByUser = Globals.Db.GetAllStockOwnedByUser(Globals.SelectedPortfolio);

                decimal maxQty = (decimal)SelectedPortfolio.Cash / (decimal)SelectedStock.Ask;

                maxQty = Math.Floor(maxQty);

                if (Quantity != 0)
                {

                    if ((Quantity * SelectedStock.Ask) <= SelectedPortfolio.Cash)
                    {

                        if (SymbolStringLIstOwnedByUser.Contains(SelectedStock.Symbol, StringComparer.OrdinalIgnoreCase))
                        {
                            //adds transaction record and updates cash in portfolio
                            Globals.Db.AddBuyTransaction(SelectedPortfolio, SelectedStock, Quantity);

                            //adds stock into users portfolio
                            Globals.Db.UpdatePortfolioStock(SelectedPortfolio, SelectedStock, Quantity);

                        }
                        else
                        {
                            //adds transaction record and updates cash in portfolio
                            Globals.Db.AddBuyTransaction(SelectedPortfolio, SelectedStock, Quantity);

                            //updates stock volume and average price in portfolio
                            Globals.Db.AddPortfolioStock(SelectedPortfolio, SelectedStock, Quantity);


                        }

                        tbQuantityBuy.Text = "";
                        RefreshStockOwnedByPortfolio();
                        UpdateUserBalance();
                        UpdatePortfolioInfo();

                        MessageBox.Show("Transaction completed", "Confirmation", MessageBoxButton.OK);


                    }
                    else
                    {

                        MessageBox.Show("You can buy only:  " + maxQty + "  Shares of: " + SelectedStock.Name, "Confirmation", MessageBoxButton.OK);
                    }

                }
                else
                {

                    MessageBox.Show("Qty cannot be 0", "Confirmation", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid Qty", "Confirmation", MessageBoxButton.OK);
            }

        }


        private void UpdatePortfolioInfo()
        {

            Entities.Portfolio updatedPortfolio = Model.DBA_Portfolio.GetUpdatedPortfolio(Globals.SelectedPortfolio);

            lbCash.Content = updatedPortfolio.Cash;
            lbGanesLooses.Content = updatedPortfolio.Balance;
            lbNet.Content = updatedPortfolio.Net;

        }

        private void UpdateUserBalance()
        {
            decimal newNet = 0;
            decimal totalSum = 0;
            decimal newBalance = 0;

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

            newNet = currentCash + totalSum;
            newBalance = (currentCash + totalSum) - 50000;

            Model.DBA_Portfolio.UpdateBalance(newBalance, Globals.SelectedPortfolio);
            Model.DBA_Portfolio.UpdateNet(newNet, Globals.SelectedPortfolio);

        }


        private void lvStockOwnedByUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Entities.PortfolioStock SelectedStock = (Entities.PortfolioStock)lvStockOwnedByUser.SelectedItem;

            if (lvStockQuotesList.SelectedItem == null)
            {
                btnSell.IsEnabled = false;
                lbAskBuyOrder.Content = "...";
                lbBidSellOrder.Content = "....";
                lblCompanyNameBuyOrder.Content = ".....";
            }

            else
            {
                if (SelectedStock == null)
                {
                    btnSell.IsEnabled = false;
                    lbAskBuyOrder.Content = "...";
                    lbBidSellOrder.Content = "....";
                    lblCompanyNameBuyOrder.Content = ".....";
                }else
                {
                btnSell.IsEnabled = true;
                lbBidSellOrder.Content = SelectedStock.CurrentBId;
                lbAskSellOrder.Content = SelectedStock.AveragePurchasedPrice;
                lblCompanyNameSellOrder.Content = SelectedStock.Symbol;
                }


            }

        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            Entities.Portfolio userPortfolio = Model.DBA_Portfolio.GetUpdatedPortfolio(Globals.SelectedPortfolio);
            int quantity;

            // this part is cheking if record already exists in database
            // if exists it updates record
            // if not it adds new record

            if (int.TryParse(tbQuantitySell.Text, out quantity))
            {

                if (quantity!=0)
                {
                String symbol;
                decimal sellPrice = 0;


                Entities.PortfolioStock SelectedStockOwnedByUSer = (Entities.PortfolioStock)lvStockOwnedByUser.SelectedItem;
                List<Entities.StockDb> DatabasePrices = Globals.Db.GetAllStockPricesFromDatabase();


                if (SelectedStockOwnedByUSer.SharesOwned >= quantity)
                {
                symbol = SelectedStockOwnedByUSer.Symbol;

                foreach (Entities.StockDb E in DatabasePrices)
                {
                    if (E.Symbol == symbol)
                    {
                        sellPrice = (decimal)E.Bid;
                    }
                }

                Globals.Db.AddSellTransaction(symbol, quantity, sellPrice, SelectedStockOwnedByUSer, userPortfolio);

                Globals.Db.DelteAllRecordWhereQtyIsZeroFromPortfolio();
                RefreshStockOwnedByPortfolio();
                UpdateUserBalance();
                UpdatePortfolioInfo();

                tbQuantitySell.Text = "";
                UpdatePortfolioInfo();
                MessageBox.Show("Transaction completed", "Confirmation", MessageBoxButton.OK);
                }else
                {
                    MessageBox.Show("You can sell only: " + SelectedStockOwnedByUSer.SharesOwned, "Confirmation", MessageBoxButton.OK);
                }
                }else
                {
                    MessageBox.Show("Qty cannot be 0", "Confirmation", MessageBoxButton.OK);
                }


            }
            else
            {
                MessageBox.Show("Invalid Qty", "Confirmation", MessageBoxButton.OK);
            }



        }


        private void RefreshTransactions()
        {
            lvTransactions.ItemsSource = Model.DBA_Transactions.GetAll(Globals.SelectedPortfolio);

        }

        private void tbSearchStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = tbSearchStock.Text.ToLower();
            if (search == "")
            {
                lvStockQuotesList.ItemsSource = Globals.Db.GetAllStockPricesFromDatabase();
            }
            else
            {
                List<Entities.StockDb> StockDbList = Globals.Db.GetAllStockPricesFromDatabase();
                
                var filteredList = from s in StockDbList
                                   where s.Symbol.ToLower().Contains(search) || s.Name.ToLower().Contains(search)
                                   select s;

                lvStockQuotesList.ItemsSource = filteredList;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetListOfStocksFromYahoo();
            RefreshStockList();
            UpdateUserBalance();
            UpdatePortfolioInfo();
        }

    }



}

