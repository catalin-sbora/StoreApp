using StoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class Seller
    {
        private readonly Store store;
        private CashRegister currentCashRegister = null;
        private readonly Stock storeStock;
        public Person PersonalData { get; set; }
        public Seller(Store store)
        {
            this.store = store;
            storeStock = store.Stock;
        }

        public void StartSell(string registerId)
        {
            currentCashRegister = store.GetCashRegister(registerId);
            currentCashRegister.StartNewSell();
        }

        public void AddProductToSell(string productId, int qty)
        {
            //validare currentCashRegister
            var product = storeStock.TakeFromStock(productId, qty);
            currentCashRegister.CurrentReceipt
                                .AddProduct(product, qty);
        }

        public Receipt GetCurrentReceipt()
        {
            if (currentCashRegister == null)
            {
                throw new NoCashRegisterSelectedException(this);
            }
            //validare currentCashRegister
            return currentCashRegister.CurrentReceipt;
        }

        public void CloseSell()
        {
            if (currentCashRegister == null)
            {
                throw new NoCashRegisterSelectedException(this);
            }
            currentCashRegister.FinalizeSell();
        }
    }
}
