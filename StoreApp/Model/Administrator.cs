using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class Administrator
    {
        private readonly Store store;
        private int lastCashRegister = 0;
        public Administrator(Store store)
        {
            this.store = store;
        }
        public void AddNewCashRegister()
        {
            lastCashRegister++;
            store.InstallNewCashRegister(lastCashRegister.ToString());
        }
        public void AddNewProductToStock(Product product, int qty)
        {
            store.Stock.Add(product, qty);
        }



    }
}
