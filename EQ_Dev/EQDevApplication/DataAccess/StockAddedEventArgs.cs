using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EQDevApplication.Model;

namespace EQDevApplication.DataAccess
{
    public class StockAddedEventArgs : EventArgs
    {
        public StockAddedEventArgs(Stock stock)
        {
            this.NewStock = stock;
        }

        public Stock NewStock { get; private set; }
    }
}
