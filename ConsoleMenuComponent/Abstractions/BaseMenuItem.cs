using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuComponent.Abstractions
{
    public abstract class BaseMenuItem: IExecutable
    {
        public BaseMenuItem(int shortCut, string text)
        {
            this.Shortcut = shortCut;
            this.Text = text;
        }
        public int Shortcut { get; }
        public string Text { get; set; }
        public abstract void Execute(object parentObject);        
    }
}
