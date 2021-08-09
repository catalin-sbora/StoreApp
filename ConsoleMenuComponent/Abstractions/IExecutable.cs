using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuComponent.Abstractions
{
    public interface IExecutable
    {
        void Execute(object parentObject);
    }
}
