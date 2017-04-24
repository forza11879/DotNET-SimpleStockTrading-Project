using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Entities
{
    class PortfolioStock
    {
        public String Symbol { get; set; }

        public int PortfolioId { get; set; }

        public int SharesOwned { get; set; }

        public decimal AveragePurchasedPrice { get; set; }

        public PortfolioStock( String symbol, int portfolioId, int sharesOwned, decimal averagePurchasedPrice)
        {
            Symbol = symbol;
            PortfolioId = portfolioId;
            SharesOwned = sharesOwned;
            AveragePurchasedPrice = averagePurchasedPrice;
        }
        //

        // computed properties, NOT stored in database
        public decimal CurrentAskPrice { get; set; }

    }
}
