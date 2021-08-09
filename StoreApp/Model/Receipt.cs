using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class Receipt
    {
        private Dictionary<string, ReceiptItem> items = new Dictionary<string, ReceiptItem>();
        public IReadOnlyCollection<ReceiptItem> Items => items.Values
                                                              .ToList()
                                                              .AsReadOnly();
        
        public decimal Total
        {
            get; private set;
        }
        public void AddProduct(Product product, int qty)
        {
            ReceiptItem item = null;
            if (items.ContainsKey(product.Id))
            {
                item = items[product.Id];                
                Total -= item.Total;
            }
            else
            {
                item = new ReceiptItem() { 
                    ProductName = product.Name,
                    PricePerUnit = product.Price
                };
                items[product.Id] = item;
            }

            item.Qty = qty;
            Total += item.Total;
        }


    }
}
