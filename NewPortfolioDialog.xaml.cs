using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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


            string name = tbName.Text;

            if (tbName.Text == "" /*|| tbName.Text  2 || tbName.Text > 50*/)
            {
                MessageBox.Show(tbName.Text + " Name input canot be empty", "Invalit Input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            else
            {

                
            }

            string email = tbEmail.Text;
            
            if (IsValid(tbEmail.Text))
            {
                MessageBox.Show(" New Portfolio successfully created", "Message Box");

            }
            else
            {
                
                MessageBox.Show(tbEmail.Text + " is not a valid email", "Invalit Input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            Entities.Portfolio p = new Entities.Portfolio() { Name = name, Email = email };
            Model.DBA_Portfolio.AddNewPortfolioToTable(p);
            //RefreshPortfolios();
            this.DialogResult = true;
        }

        public bool IsValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        //private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("^[a-zA-Z''-'\s]{1,40}$");

        //    e.Handled = !regex.IsMatch(e.Text);
        //}



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
