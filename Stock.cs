using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Stock
    {
       /*public Stock(int stockID, string symbol, string name, decimal bid, decimal ask, decimal open, decimal previousClose, decimal lastTrade, decimal high, decimal low, int volume, decimal high52, decimal low52)
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
        }*/





            //Lets Prase CSVdata to list of objects

            public static List<Stock> Parse(string csvData)
        {
            List<Stock> StockFromApiList = new List<Stock>();
            string[] rows = csvData.Replace("\r", "").Split('\n');


            foreach (string row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;

                string[] cols = row.Split(',');

                Stock s = new Stock();
                s.StockID = 12;
                s.Symbol = Convert.ToString(cols[0]);
                s.Name = cols[1];
                s.Bid = Convert.ToDecimal(cols[2]);
                s.Ask = Convert.ToDecimal(cols[3]);
                s.Open = Convert.ToDecimal(cols[4]);
                s.PreviousClose = Convert.ToDecimal(cols[5]);
                s.LastTrade = Convert.ToDecimal(cols[6]);
                s.Volume = Convert.ToInt32(cols[7]);
                s.High = Convert.ToDecimal(cols[8]);
                s.Low = Convert.ToDecimal(cols[9]);
                s.High52 = Convert.ToDecimal(cols[10]);
                s.Low52 = Convert.ToDecimal(cols[11]);   
                StockFromApiList.Add(s);
            }
            return StockFromApiList;

        }


        public int StockID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Open { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal LastTrade { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public int Volume { get; set; }
        public decimal High52 { get; set; }
        public decimal Low52 { get; set; }

        
    }
}
