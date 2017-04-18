using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Database
    {
        private SqlConnection conn;



        //Setting up database connection 
        public Database()
        {
            conn = new SqlConnection(@"Data Source=john-gri.database.windows.net;Initial Catalog=TradingDB;Persist Security Info=True;User ID=dbadmin;Password=JohnIsGreat2000");
            conn.Open();
        }



        public List<Portfolio> GetAllPortfolios()
        {
            List<Portfolio> result = new List<Portfolio>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Portfolio", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["portfolioId"];
                    string name = (string)reader["Name"];
                    string email = (string)reader["Email"];
                    decimal cash = (decimal)reader["Cash"];
                    decimal net = (decimal)reader["Net"];
                    decimal Balance = (decimal)reader["Balance"];
                    Portfolio p = new Portfolio(id, name, email, cash, net, Balance);
                    result.Add(p);
                }
            }
            return result;
        }


        //This method is used to get all Symbols from database 
        public List<String> GetAllSymbolsFromDatabase()
        {
            List<String> result = new List<String>();

            using (SqlCommand command = new SqlCommand("SELECT Symbol FROM StockQuotesTable", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }

            return result;
        }




        //Method adds Stock To Database in case if record does not exists
        public void AddStockToStockTable(Stock s)
        {

            string sql = "INSERT INTO StockQuotesTable (Symbol, [Name], Bid, Ask, [Open], PreviousClose, LastTrade, Volume, High, Low, High52, Low52)"
                        + "VALUES (@Symbol, @Name, @Bid, @Ask, @Open, @PreviousClose, @LastTrade, @Volume, @High, @Low, @High52, @Low52)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Symbol", SqlDbType.NChar).Value = s.Symbol;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = s.Name;
            cmd.Parameters.Add("@Bid", SqlDbType.Money).Value = s.Bid;
            cmd.Parameters.Add("@Ask", SqlDbType.Money).Value = s.Ask;
            cmd.Parameters.Add("@Open", SqlDbType.Money).Value = s.Open;
            cmd.Parameters.Add("@PreviousClose", SqlDbType.Money).Value = s.PreviousClose;
            cmd.Parameters.Add("@LastTrade", SqlDbType.Money).Value = s.LastTrade;
            cmd.Parameters.Add("@Volume", SqlDbType.Int).Value = s.Volume;
            cmd.Parameters.Add("@High", SqlDbType.Money).Value = s.High;
            cmd.Parameters.Add("@Low", SqlDbType.Money).Value = s.Low;
            cmd.Parameters.Add("@High52", SqlDbType.Money).Value = s.High52;
            cmd.Parameters.Add("@Low52", SqlDbType.Money).Value = s.Low52;

          cmd.ExecuteNonQuery();
        }



        //Method updates record if it already exists in database
        public void UpdateStockToStockTable(Stock s)
        {

            string sql = "UPDATE StockQuotesTable " +
                "SET Bid=@Bid, Ask=@Ask, Open=@Open, PreviuosClose=@PreviousClose, LastTrade=@LastTrade, Volume =@Volume, High=@High, Low=@Low, High52=@High52, Low52=@low52";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Bid", SqlDbType.Money).Value = s.Bid;
            cmd.Parameters.Add("@Ask", SqlDbType.Money).Value = s.Ask;
            cmd.Parameters.Add("@Open", SqlDbType.Money).Value = s.Open;
            cmd.Parameters.Add("@PreviousClose", SqlDbType.Money).Value = s.PreviousClose;
            cmd.Parameters.Add("@LastTrade", SqlDbType.Money).Value = s.LastTrade;
            cmd.Parameters.Add("@Volume", SqlDbType.Int).Value = s.Volume;
            cmd.Parameters.Add("@High", SqlDbType.Money).Value = s.High;
            cmd.Parameters.Add("@Low", SqlDbType.Money).Value = s.Low;
            cmd.Parameters.Add("@High52", SqlDbType.Money).Value = s.High52;
            cmd.Parameters.Add("@Low52", SqlDbType.Money).Value = s.Low52;

            cmd.ExecuteNonQuery();
        }





    }




}
