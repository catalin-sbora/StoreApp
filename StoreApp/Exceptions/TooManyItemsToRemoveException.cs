using StoreApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Exceptions
{
    public class TooManyItemsToRemoveException: Exception
    {

        public StockItem ExistingStockItem { get; private set; }
        public int QtyToTake { get; private set; }       

        public TooManyItemsToRemoveException(string message, StockItem existingItem, int qtyToTake) : base(message)
        {
            ExistingStockItem = existingItem;
            QtyToTake = qtyToTake;
        }
    }
}
