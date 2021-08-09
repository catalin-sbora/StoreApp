using StoreApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Exceptions
{
    public class NoCashRegisterSelectedException: Exception
    {
        public Seller CurrentSeller { get; private set; }
        public NoCashRegisterSelectedException(Seller currentSeller): base($"No cash register selected.")
        {
            CurrentSeller = currentSeller;
        }

    }
}
