using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
                    decimal balance = (decimal)reader["Balance"];
                    Portfolio p = new Portfolio(id, name, email, cash, net, balance);
                    result.Add(p);
                }
            }
            return result;
        }



       public List<StockDb> GetAllStockPricesFromDatabase()
        {
            List<StockDb> result = new List<StockDb>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM StockQuotesTable", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["StockId"];
                    string symbol = (string)reader["Symbol"];
                    string name = (string)reader["Name"];
                    decimal bid = (decimal)reader["Bid"];
                    decimal ask = (decimal)reader["Ask"];
                    decimal open = (decimal)reader["Open"];
                    decimal previousClose = (decimal)reader["PreviousClose"];
                    decimal lastTrade = (decimal)reader["LastTrade"];
                    decimal high = (decimal)reader["High"];
                    decimal low = (decimal)reader["Low"];
                    int volume = (int)reader["Volume"];
                    decimal high52 = (decimal)reader["High52"];
                    decimal low52 = (decimal)reader["Low52"];
                    StockDb s = new StockDb(id, symbol, name, bid, ask, open, previousClose, lastTrade, high, low, volume, high52, low52);
                    result.Add(s);
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
        public void AddStockToStockTable(YahooStock s)
        {

            string sql = "INSERT INTO StockQuotesTable (Symbol, [Name], Bid, Ask, [Open], PreviousClose, LastTrade, Volume, High, Low, High52, Low52)"
                        + "VALUES (@Symbol, @Name, @Bid, @Ask, @Open, @PreviousClose, @LastTrade, @Volume, @High, @Low, @High52, @Low52)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Symbol", SqlDbType.NChar).Value = s.Symbol.Trim();
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = s.Name.Trim();
            cmd.Parameters.Add("@Bid", SqlDbType.Money).Value = s.Bid ?? SqlMoney.Null;
            cmd.Parameters.Add("@Ask", SqlDbType.Money).Value = s.Ask ?? SqlMoney.Null;
            cmd.Parameters.Add("@Open", SqlDbType.Money).Value = s.Open ?? SqlMoney.Null;
            cmd.Parameters.Add("@PreviousClose", SqlDbType.Money).Value = s.PreviousClose ?? SqlMoney.Null;
            cmd.Parameters.Add("@LastTrade", SqlDbType.Money).Value = s.LastTrade ?? SqlMoney.Null;
            cmd.Parameters.Add("@Volume", SqlDbType.Int).Value = s.Volume;
            cmd.Parameters.Add("@High", SqlDbType.Money).Value = s.High ?? SqlMoney.Null;
            cmd.Parameters.Add("@Low", SqlDbType.Money).Value = s.Low ?? SqlMoney.Null;
            cmd.Parameters.Add("@High52", SqlDbType.Money).Value = s.High52 ?? SqlMoney.Null;
            cmd.Parameters.Add("@Low52", SqlDbType.Money).Value = s.Low52 ?? SqlMoney.Null;

            cmd.ExecuteNonQuery();
        }



        //Method updates record if it already exists in database
        public void UpdateStockToStockTable(YahooStock s)
        {

            string sql = "UPDATE StockQuotesTable " +
                "SET Bid=@Bid, Ask=@Ask, [Open]=@Open, PreviousClose=@PreviousClose, LastTrade=@LastTrade, Volume =@Volume, High=@High, Low=@Low, High52=@High52, Low52=@low52 " +
                "WHERE Symbol=@Symbol";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Symbol", SqlDbType.NChar).Value = s.Symbol;
            cmd.Parameters.Add("@Bid", SqlDbType.Money).Value = s.Bid ?? SqlMoney.Null;
            cmd.Parameters.Add("@Ask", SqlDbType.Money).Value = s.Ask ?? SqlMoney.Null;
            cmd.Parameters.Add("@Open", SqlDbType.Money).Value = s.Open ?? SqlMoney.Null;
            cmd.Parameters.Add("@PreviousClose", SqlDbType.Money).Value = s.PreviousClose ?? SqlMoney.Null;
            cmd.Parameters.Add("@LastTrade", SqlDbType.Money).Value = s.LastTrade ?? SqlMoney.Null;
            cmd.Parameters.Add("@Volume", SqlDbType.Int).Value = s.Volume;
            cmd.Parameters.Add("@High", SqlDbType.Money).Value = s.High ?? SqlMoney.Null;
            cmd.Parameters.Add("@Low", SqlDbType.Money).Value = s.Low ?? SqlMoney.Null;
            cmd.Parameters.Add("@High52", SqlDbType.Money).Value = s.High52 ?? SqlMoney.Null;
            cmd.Parameters.Add("@Low52", SqlDbType.Money).Value = s.Low52 ?? SqlMoney.Null;

            cmd.ExecuteNonQuery();
        }





    }




}
