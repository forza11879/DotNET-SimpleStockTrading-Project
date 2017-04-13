using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Stock
    {
        int StockID;
        String Symbol;
        String Name;
        decimal AskPrice;
        decimal BitPrice;

        public Stock(int stockID, string symbol, string name, decimal askPrice, decimal bitPrice)
        {
            StockID = stockID;
            Symbol = symbol;
            Name = name;
            AskPrice = askPrice;
            BitPrice = bitPrice;
        }
    }
}
