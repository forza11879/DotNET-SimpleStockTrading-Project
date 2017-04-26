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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TradingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Globals.Db = new Database(); // FIXME handle exception and show dialog
            RefreshPortfolios();
        }

        // Lets get all Portfolios from database
        private void RefreshPortfolios()
        {
            lvPortfolios.ItemsSource = Model.DBA_Portfolio.GetAll();
        }

        private void lvPortfolios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //getting selected object from ListView
            Globals.SelectedPortfolio = (Entities.Portfolio)lvPortfolios.SelectedItem;

            //assigning needed info from selected objcet to labels

            //Balance
            lbBalance.Content = Globals.SelectedPortfolio.Balance;


            //Cash
            lbCash.Content = Globals.SelectedPortfolio.Cash;

            //Net
            lbNet.Content = Globals.SelectedPortfolio.Net;

        }

        // Action opens new window and sends infromation
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {

            if (lvPortfolios.SelectedIndex >= 0)
            {
                Globals.SelectedPortfolio = (Entities.Portfolio)lvPortfolios.SelectedItem;
                HomeWindow win2 = new HomeWindow();
                win2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No portfolio is selected", "Confirmation", MessageBoxButton.OK);
            }


        }


        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NewPortfolioDialog win3 = new NewPortfolioDialog();
            win3.Show();
            this.Close();
        }

    }
}
