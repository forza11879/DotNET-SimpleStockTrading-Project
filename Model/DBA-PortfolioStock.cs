using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Model
{
    class DBA_PortfolioStock
    {

        public static List<Entities.PortfolioStock> GetAll(Entities.Portfolio p)
        {
            List<Entities.PortfolioStock> result = new List<Entities.PortfolioStock>();


            string SelecetUserPortfolio = "SELECT * FROM PortfolioStock WHERE GameID=@GameID";
            SqlCommand cmdGetPortfolioStock = new SqlCommand(SelecetUserPortfolio, Globals.Db.conn);
            cmdGetPortfolioStock.Parameters.Add("@GameID", SqlDbType.Int).Value = p.PortfolioID;

            using (SqlDataReader reader = cmdGetPortfolioStock.ExecuteReader())

            {
                while (reader.Read())
                {
                    string symbol = (string)reader["Symbol"];
                    Console.Write(symbol);
                    int id = Convert.ToInt32(reader["GameID"]);
                    int numberOfSharesOwned = Convert.ToInt32(reader["NumberOfSharesOwned"]);
                    Decimal averagePurchasePrice = Convert.ToDecimal(reader["averagePurchasePrice"]);
                    Entities.PortfolioStock pn = new Entities.PortfolioStock(symbol, id, numberOfSharesOwned, averagePurchasePrice);
                    //pn.AveragePurchasedPrice = 
                    result.Add(pn);
                }
            }
            return result;
        }





    }
}