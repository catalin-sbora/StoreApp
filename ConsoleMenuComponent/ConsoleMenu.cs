using ConsoleMenuComponent.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleMenuComponent
{
    public class ConsoleMenu : BaseMenuItem
    {
        private List<BaseMenuItem> menuItems = new List<BaseMenuItem>();
        private bool continueExecution = false;
        public ConsoleMenu(List<BaseMenuItem> menuElements):this(0,"", menuElements)
        {       
            
        }
        public ConsoleMenu(int shortCut, string text, List<BaseMenuItem> menuElements):base(shortCut, text)
        {
            menuItems.Add(new ConsoleMenuItem(0,
                                          "Exit",
                                          (parent) => { continueExecution = false; }
                                         )
                    );
            menuItems.AddRange(menuElements);
        }
        private void DisplayMenu()
        {
            Console.Clear();
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine($"{menuItem.Shortcut}. {menuItem.Text}");
            }

            Console.WriteLine("Please enter your option: ");            
        }

        private int ReadCurrentOption()
        {
            var currentKey = Console.ReadKey();
            var keyCode = currentKey.KeyChar - '0';
            return keyCode;
        }

        private void DisplayInvalidOption()
        {
            Console.WriteLine("Invalid option. Please select a value in the menu");
        }
        public override void Execute(object parentObject)
        {
            continueExecution = true;
            while (continueExecution)
            {
                DisplayMenu();
                var currentOption = ReadCurrentOption();
                var currentItem  = menuItems.SingleOrDefault(menuItem => menuItem.Shortcut == currentOption);
                if (currentItem == null)
                {
                    DisplayInvalidOption();
                    continue;
                }
                currentItem.Execute(parentObject);
            }
        }
    }
}
