using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class PortfolioStock
    {
        int StockOwnedId;
        int SymbolId;
        int PortfolioId;
        int SharesOwned;
        decimal PurchasedPrice;

        public PortfolioStock(int stockOwnedId, int symbolId, int portfolioId, int sharesOwned, decimal purchasedPrice)
        {
            StockOwnedId = stockOwnedId;
            SymbolId = symbolId;
            PortfolioId = portfolioId;
            SharesOwned = sharesOwned;
            PurchasedPrice = purchasedPrice;
        }
    }
}
