using StoreApp.UI;
using System;

namespace StoreApp
{
    class Program
    {

        static void Main(string[] args)
        {
            AppMenu appMenu = new AppMenu();
            appMenu.Initialize();
            appMenu.Run();

            Console.WriteLine("Hello World!");
        }
    }
}
