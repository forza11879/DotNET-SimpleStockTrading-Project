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

namespace TradingApp
{
    /// <summary>
    /// Interaction logic for NewPortfolioDialog.xaml
    /// </summary>
    public partial class NewPortfolioDialog : Window
    {
        public NewPortfolioDialog()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void ButtonOK_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string name = tbName.Text;
        //        string email = tbEmail.Text;
        //        decimal cash = decimal.Parse(tbCash.Text);


        //        Entities.Portfolio b = new Entities.Portfolio() { Name = name, Email = email, Cash = cash};
        //        Model.DBA_Portfolio.AddPortfoliosToTable(b);
        //        RefreshPortfolios();
        //    }
        //    catch (SqlException ex)
        //    {

        //        Console.WriteLine(ex.StackTrace);
        //        MessageBox.Show("Database query error " + ex.Message);

        //    }
        //}

    }
}
