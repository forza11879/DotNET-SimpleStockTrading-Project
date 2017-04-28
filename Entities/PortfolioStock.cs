using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TradingApp.Entities
{
    class PortfolioStock
    {
        public String Symbol { get; set; }

        public int PortfolioId { get; set; }

        public int SharesOwned { get; set; }

        public decimal AveragePurchasedPrice { get; set; }

        public PortfolioStock(String symbol, int portfolioId, int sharesOwned, decimal averagePurchasedPrice)
        {
            Symbol = symbol;
            PortfolioId = portfolioId;
            SharesOwned = sharesOwned;
            AveragePurchasedPrice = averagePurchasedPrice;
        }
        //

        // computed properties, NOT stored in database
        public decimal TotalValue
        {
            get
            {
                return AveragePurchasedPrice * SharesOwned;

            }
        }

        public decimal CurrentBId
        {
            get
            {
                decimal currentbid =0;
                List <Entities.StockDb> DbStockList = Globals.Db.GetAllStockPricesFromDatabase();

                var myItem = DbStockList.Find(StockDb => StockDb.Symbol == Symbol);

                currentbid =(decimal) myItem.Bid;

                return currentbid;
            }
        }


        public decimal MarketVaue
        {
            get
            {
                return CurrentBId * SharesOwned;
            }
        }

        public decimal GainsandLooses
        {
            get
            {
                return MarketVaue - TotalValue;
            }

        }

    }
}
