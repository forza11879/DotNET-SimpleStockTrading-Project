using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            // caller must handle ParseException
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
                try
                {
                s.Symbol = Convert.ToString(cols[0]);
                s.Name = cols[1];
                s.Bid = (cols[2].ToUpper().Contains("N/A")) ? (decimal?) null : Convert.ToDecimal(cols[2]);
                s.Ask = (cols[3].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[3]);
                s.Open = (cols[4].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[4]);
                s.PreviousClose = (cols[5].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[5]);
                s.LastTrade = (cols[6].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[6]);
                s.Volume = (cols[7].ToUpper().Contains("N/A")) ? (int?) null : Convert.ToInt32(cols[7]);
                s.High = (cols[8].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[8]);
                s.Low = (cols[9].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[9]);
                s.High52 = (cols[10].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[10]);
                s.Low52 = (cols[11].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[11]);
                StockFromApiList.Add(s);
                } catch (FormatException e)
                {
                    MessageBox.Show("Unable to conver value", "Confirmation", MessageBoxButton.OK);
                    Console.Write(e.StackTrace);
                }

            }
            return StockFromApiList;

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
