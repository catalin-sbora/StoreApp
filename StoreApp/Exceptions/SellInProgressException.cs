using StoreApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Exceptions
{
    public class SellInProgressException: Exception
    {
        public CashRegister Register { get; private set; }
        public Receipt SellReceipt { get; private set; }

        public SellInProgressException(string message, CashRegister register, Receipt sellReceipt) : base(message)
        {
            Register = register;
            SellReceipt = sellReceipt;
        }
    }
}
