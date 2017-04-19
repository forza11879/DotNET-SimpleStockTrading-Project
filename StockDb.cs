using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class StockDb
    {
        public StockDb(int stockID, string symbol, string name, decimal? bid, decimal? ask, decimal? open, decimal? previousClose, decimal? lastTrade, decimal? high, decimal? low, int? volume, decimal? high52, decimal? low52)
        {
            StockID = stockID;
            Symbol = symbol;
            Name = name;
            Bid = bid;
            Ask = ask;
            Open = open;
            PreviousClose = previousClose;
            LastTrade = lastTrade;
            High = high;
            Low = low;
            Volume = volume;
            High52 = high52;
            Low52 = low52;
        }

        public int StockID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Ask { get; set; }
        public decimal? Open { get; set; }
        public decimal? PreviousClose { get; set; }
        public decimal? LastTrade { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public int? Volume { get; set; }
        public decimal? High52 { get; set; }
        public decimal? Low52 { get; set; }

    }
}
