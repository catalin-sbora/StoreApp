using StoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class Stock
    {

        private Dictionary<string, StockItem> items = new Dictionary<string, StockItem>();

        public IReadOnlyCollection<StockItem> StockItems => items.Values
                                                          .ToList()
                                                          .AsReadOnly();

        public decimal TotalValue { get; private set; }


        public void Add(Product productToAdd, int qty)
        {
            if (productToAdd == null)
                throw new ArgumentNullException("productToAdd","Invalid product specified");

            StockItem item = null;
            if (items.ContainsKey(productToAdd.Id))
            {
                item = items[productToAdd.Id];
                TotalValue -= item.Total;
            }
            else
            {
                item = new StockItem { Product = productToAdd, Qty = 0 };
                items[productToAdd.Id] = item;
            }
            item.Qty += qty;
            TotalValue += item.Total;
        }

        public Product TakeFromStock(string productId, int qty)
        {
            if (!items.ContainsKey(productId))
                throw new ArgumentException("Invalid product identifier.", "productId");

            var item = items[productId];
            if (qty > item.Qty)
            {
                throw new TooManyItemsToRemoveException("Insuficient qty available", item, qty);
            }
            TotalValue -= item.Total;
            item.Qty -= qty;
            TotalValue += item.Total;

            return item.Product;
        }

        public void Remove(string productId)
        {
            items.Remove(productId);
        }

    }
}
