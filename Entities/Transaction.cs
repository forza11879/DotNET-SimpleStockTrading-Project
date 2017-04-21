using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Entities
{
    class Transaction
    {
        int TransactionId;
        int PortfolioId;
        String Type;
        String Symbol;
        decimal PurchasePrice;
        int SharesBought;
        DateTime Date;

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
