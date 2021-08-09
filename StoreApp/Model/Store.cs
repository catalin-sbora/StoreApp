using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class Store
    {
        private readonly Dictionary<string, CashRegister> cashRegisters = new Dictionary<string, CashRegister>();
        public string Name { get; private set; }
        public Stock Stock { get; private set; }
        public IReadOnlyCollection<CashRegister> CashRegisters => cashRegisters.Values
                                                                               .ToList()
                                                                               .AsReadOnly();

        public Store()
        {
            Stock = new Stock();
        }

        public void LoadStock(List<StockItem> stockItems)
        {
            foreach (var stockItem in stockItems)
            {
                Stock.Add(stockItem.Product, stockItem.Qty);
            }
        }

        public void LoadDefaultCashRegisters()
        {
            InstallNewCashRegister("1");
        }
        public void InstallNewCashRegister(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException($"Invalid identifier provided for a cash register");
            }
            if (cashRegisters.ContainsKey(identifier))
            {
                throw new ArgumentException($"Cash register with the given identifier {identifier} already installed", "identifier");
            }
            cashRegisters[identifier] = new CashRegister(identifier);
        }

        public CashRegister GetCashRegister(string id)
        {
            if (cashRegisters.ContainsKey(id))
            {
                return cashRegisters[id];
            }
            throw new ArgumentException($"Invalid register identifier {id}", "id");
        }

        

    }
}
