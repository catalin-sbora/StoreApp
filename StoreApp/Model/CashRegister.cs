using StoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Model
{
    public class CashRegister
    {
        private List<Receipt> receipts = new List<Receipt>();
        public string Id { get; private set; }
        public Receipt CurrentReceipt { get; private set; }

        public IReadOnlyCollection<Receipt> Receipts => receipts.AsReadOnly();
        
        public CashRegister(string identifier)
        {
            Id = identifier;
        }

        public void StartNewSell()
        {
            if (CurrentReceipt != null)
            {
                throw new SellInProgressException($"A sell is in progress. The current sell must be finalized before starting a new sell", this, CurrentReceipt);
            }
            CurrentReceipt = new Receipt();
        }

        
        public void FinalizeSell()
        {
            if (CurrentReceipt == null)
            {
                throw new NoSellInProgressException($"No sell is in progress to finalize.", this);   
            }
            receipts.Add(CurrentReceipt);
            CurrentReceipt = null;
        }

        public bool IsSellInProgress => (CurrentReceipt != null);



    }
}
