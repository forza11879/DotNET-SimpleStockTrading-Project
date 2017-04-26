using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Model
{
    class DBA_Transactions
    {
        public static List<Entities.Transaction> GetAll(Entities.Portfolio p)
        {
            List<Entities.Transaction> result = new List<Entities.Transaction>();
            string sql = "SELECT * FROM Transactions WHERE PortfolioID=@PortfolioID";

            SqlCommand cmdGetAllTransactions = new SqlCommand(sql, Globals.Db.conn);
              cmdGetAllTransactions.Parameters.Add("@PortfolioId", SqlDbType.Int).Value =p.PortfolioID;

            using (SqlDataReader reader = cmdGetAllTransactions.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    int portfolioID = (int)reader["portfolioId"];
                    string type = (string)reader["Type"];
                    string symbol = (string)reader["Symbol"];
                    decimal price = (decimal)reader["BuySellPrice"];
                    int sharesBoughtSold = (int)reader["SharesBoughtSold"];
                    DateTime date = (DateTime)reader["date"];
                    Entities.Transaction t = new Entities.Transaction(id, portfolioID, type, symbol, price, sharesBoughtSold, date);
                    result.Add(t);
                }
            }
            return result;
        }
    

    }
}
