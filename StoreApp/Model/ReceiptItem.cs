using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class ReceiptItem
    {
        private int qty = 0;
        private decimal pricePerUnit = 0.0M;

        public string ProductName { get; set; }
        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
                Total = qty * PricePerUnit;
            }
        }
        public decimal PricePerUnit 
        {
            get
            {
                return pricePerUnit;
            }
            set
            {
                pricePerUnit = value;
                Total = Qty * pricePerUnit;
            }
        }

        public decimal Total 
        {
            get; private set;
        }
    }
}
