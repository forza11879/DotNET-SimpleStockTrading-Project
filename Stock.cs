using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Stock
    {
        public Stock(int stockID, string symbol, string name, decimal bid, decimal ask, decimal lastTrade, decimal change, decimal changePercentage, decimal high, decimal low,
            int volume, int marketCap, decimal high52,decimal low52)
        {
            StockID = stockID;
            Symbol = Symbol;
            Name = name;
            Bid = bid;
            Ask = ask;
            LastTrade = lastTrade;
            Change = change;
            ChangePercentage = changePercentage;
            High = high;
            Low = low;
            Volume = volume;
            High52 = high52;
            Low52 = low52;



        }


        public int StockID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal LastTrade { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public int Volume { get; set; }
        public int MarketCap { get; set; }
        public decimal High52 { get; set; }
        public decimal Low52 { get; set; }

        
    }
}
