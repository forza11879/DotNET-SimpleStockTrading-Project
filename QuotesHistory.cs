using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;







namespace TradingApp
{
    public class QuotesHistory
    {
        //int IdHistory;
        //int SymbolId;
        //DateTime Date;
        //decimal OpeningPrice;
        //decimal ClosingPrice;
        //decimal High;
        //decimal Low;
        //int Volume;
        //decimal Change;

        int IdHistory { get; set; }
        public DateTime Date { get; set; }
        public double OpeningPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double ClosingPrice { get; set; }
        public double Volume { get; set; }
        public double AdjClose { get; set; }

        //public QuotesHistory(int idHistory, int symbolId, DateTime date, double openingPrice, double closingPrice, double high, double low, int volume/*, decimal change*/, double adjClose)
        //{
        //    IdHistory = idHistory;
        //    //SymbolId = symbolId;
        //    Date = date;
        //    OpeningPrice = openingPrice;
        //    High = high;
        //    Low = low;
        //    ClosingPrice = closingPrice;
        //    Volume = volume;
        //    //Change = change;
        //    AdjClose = adjClose;

        //}
    }

    public class QuotesHistoryLoader
    {
        public static List LoadQuotesHistory(string ticker, int yearToStartFrom)
        {
            List retval = new List();

            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", ticker, yearToStartFrom));

                data = data.Replace("r", "");

                string[] rows = data.Split('n');

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
                    qh.Volume = Convert.ToDouble(cols[5]);
                    qh.AdjClose = Convert.ToDouble(cols[6]);

                    retval.Add(qh);
                }

                return retval;
            }
        }
    }
}
