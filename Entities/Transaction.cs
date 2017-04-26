using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Entities
{
    class Transaction
    {
       public int TransactionId { get; set; }
       public int PortfolioId { get; set; }
       public String Type { get; set; }
       public String Symbol { get; set; }
       public decimal PurchasePrice { get; set; }
       public int SharesBought { get; set; }
       public DateTime Date { get; set; }

        public Transaction(int transactionId, int portfolioId, string type, string symbol, decimal purchasePrice, int sharesBought, DateTime date)
        {
            TransactionId = transactionId;
            PortfolioId = portfolioId;
            Type = type;
            Symbol = symbol;
            PurchasePrice = purchasePrice;
            SharesBought = sharesBought;
            Date = date;
        }
    }
}
