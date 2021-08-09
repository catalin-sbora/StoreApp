using ConsoleMenuComponent.Abstractions;
using System;


namespace ConsoleMenuComponent
{
    public class ConsoleMenuItem: BaseMenuItem
    {
        private Action<object> executeAction;
        public ConsoleMenuItem(int shortCut, string text, Action<object> executeAction): base(shortCut, text)
        {            
            this.executeAction = executeAction;
        }     
        public override void  Execute(object parentObject)
        {
            executeAction?.Invoke(parentObject);
        }
    }
}
