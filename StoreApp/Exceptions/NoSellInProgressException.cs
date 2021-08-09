using StoreApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Exceptions
{
    public class NoSellInProgressException: Exception
    {
        public CashRegister Register { get; private set; }
        public NoSellInProgressException(string message, CashRegister register): base(message)
        {
            Register = register;
        }
    }
}
