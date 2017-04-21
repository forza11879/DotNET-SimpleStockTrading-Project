using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Entities
{
    class PortfolioStock
    {
        int SymbolId;
        int PortfolioId;
        int SharesOwned;
        decimal AveragePurchasedPrice;

        public PortfolioStock( int symbolId, int portfolioId, int sharesOwned, decimal averagePurchasedPrice)
        {
            SymbolId = symbolId;
            PortfolioId = portfolioId;
            SharesOwned = sharesOwned;
            AveragePurchasedPrice = averagePurchasedPrice;
        }
    }
}
