using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Model
{
    class DBA_Portfolio
    {
        public static List<Entities.Portfolio> GetAll()
        {
            List<Entities.Portfolio> result = new List<Entities.Portfolio>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Portfolio", Globals.Db.conn))
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
                    Entities.Portfolio p = new Entities.Portfolio(id, name, email, cash, net, balance);
                    result.Add(p);
                }
            }
            return result;
        }

    }
}
