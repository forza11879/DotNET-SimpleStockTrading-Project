using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Portfolio
    {
        //must be public
        // must have getters and setters in order to access it from listview
       public int PortfolioID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public decimal Cash { get; set; }
        public decimal Net { get; set; }
        public decimal Balance { get; set; }

        public Portfolio(int portfolioID, string name, string email, decimal cash, decimal net, decimal balance)
        {
            PortfolioID = portfolioID;
            Name = name;
            Email = email;
            Cash = cash;
            Net = net;
            Balance = balance;
        }
    }
}
