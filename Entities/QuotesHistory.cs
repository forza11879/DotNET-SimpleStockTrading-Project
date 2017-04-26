using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Entities
{
    public class QuotesHistory
    {
       

        public int IdHistory { get; set; }
        public DateTime Date { get; set; }
        public double OpeningPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double ClosingPrice { get; set; }
        public Int64 Volume { get; set; }
        public double AdjClose { get; set; }

        public QuotesHistory(int idHistory, DateTime date, double openingPrice, double closingPrice, double high, double low, Int64 volume, double adjClose)
        {
            IdHistory = idHistory;
            Date = date;
            OpeningPrice = openingPrice;
            High = high;
            Low = low;
            ClosingPrice = closingPrice;
            Volume = volume;
            AdjClose = adjClose;

        }

        public QuotesHistory()
        {


        }
    }

    public class QuotesHistoryLoader
    {
        public static List<QuotesHistory> LoadQuotesHistory(string symbol, int yearToStart)
        {
            List<QuotesHistory> quoteHistoryList = new List<QuotesHistory>();

            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", symbol, yearToStart));

                data = data.Replace("\r", "");

                string[] rows = data.Split('\n');

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    QuotesHistory qh = new QuotesHistory();

                    qh.Date = Convert.ToDateTime(cols[0]);
                    qh.OpeningPrice = Convert.ToDouble(cols[1]);
                    qh.High = Convert.ToDouble(cols[2]);
                    qh.Low = Convert.ToDouble(cols[3]);
                    qh.ClosingPrice = Convert.ToDouble(cols[4]);
                    qh.Volume = Convert.ToInt64(cols[5]);
                    qh.AdjClose = Convert.ToDouble(cols[6]);

                    quoteHistoryList.Add(qh);
                }

                return quoteHistoryList;
            }
        }
    }
}
