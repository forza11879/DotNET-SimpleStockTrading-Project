using System;
using System.Collections.Generic;
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

        public void AddStockToStockTavle(Stock s)
        {
            string sql = "INSERT INTO books (GenreId, Author, Title, Price, PublishDate, Description) "
                        + " VALUES (@GenreId, @Author, @Title, @Price, @PublishDate, @Description)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@GenreId", SqlDbType.Int).Value = b.GenreId;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = b.Author;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = b.Title;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = b.Price;
            cmd.Parameters.Add("@PublishDate", SqlDbType.Date).Value = b.PublishDate;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = b.Description;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }





    }




}
