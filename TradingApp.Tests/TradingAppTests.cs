using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TradingApp.Tests
{
    [TestClass]
    public class TradingAppTests
    {
        [TestMethod]
        public void HomeWindow_btnSell_Click()
        {
            //arrange
            int numberOfStockQtyOwnedByUser = 15;
            int numberOfStockUserSelling = 10;


            //act
            if ((numberOfStockQtyOwnedByUser- numberOfStockUserSelling)<0)
            {
                throw new ArgumentException("You cannot sell more than what you have");
            }

        }
    }
}
