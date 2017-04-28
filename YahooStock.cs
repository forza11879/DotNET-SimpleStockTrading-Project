using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;





namespace TradingApp
{
    class YahooStock
    {


        //Lets Prase CSVdata to list of objects

        // caller must handle ParseException
        public static List<YahooStock> Parse(string csvData)
        {


            List<YahooStock> StockFromApiList = new List<YahooStock>();
            string[] rows = csvData.Replace("\r", "").Split('\n');


            foreach (string row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;

                string[] cols = row.Split(',');

                YahooStock s = new YahooStock();
                s.StockID = 12;
                try
                {
                    s.Symbol = Convert.ToString(cols[0]).Trim('"');
                    s.Name = Convert.ToString(cols[1]).Trim('"');
                    s.Bid = (cols[2].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[2]);
                    s.Ask = (cols[3].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[3]);
                    s.Open = (cols[4].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[4]);
                    s.PreviousClose = (cols[5].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[5]);
                    s.LastTrade = (cols[6].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[6]);
                    s.Volume = (cols[7].ToUpper().Contains("N/A")) ? (int?)null : Convert.ToInt32(cols[7]);
                    s.High = (cols[8].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[8]);
                    s.Low = (cols[9].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[9]);
                    s.High52 = (cols[10].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[10]);
                    s.Low52 = (cols[11].ToUpper().Contains("N/A")) ? (decimal?)null : Convert.ToDecimal(cols[11]);
                    StockFromApiList.Add(s);
                }
                catch (FormatException e)
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
