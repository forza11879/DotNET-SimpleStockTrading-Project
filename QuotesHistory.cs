using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class QuotesHistory
    {
        int IdHistory;
        int SymbolId;
        DateTime Date;
        decimal OpeningPrice;
        decimal ClosingPrice;
        decimal High;
        decimal Low;
        int Volume;
        decimal Change;

        public QuotesHistory(int idHistory, int symbolId, DateTime date, decimal openingPrice, decimal closingPrice, decimal high, decimal low, int volume, decimal change)
        {
            IdHistory = idHistory;
            SymbolId = symbolId;
            Date = date;
            OpeningPrice = openingPrice;
            ClosingPrice = closingPrice;
            High = high;
            Low = low;
            Volume = volume;
            Change = change;
        }
    }
}
