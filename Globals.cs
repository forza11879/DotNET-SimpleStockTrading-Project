using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp
{
    class Globals
    {
        private static Model.Database _db;
        public static Model.Database Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new Model.Database();

                }
                return _db;
            }
        }
        public static Entities.Portfolio SelectedPortfolio;

        

    }
}
