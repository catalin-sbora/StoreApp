using ConsoleMenuComponent;
using ConsoleMenuComponent.Abstractions;
using StoreApp.Exceptions;
using StoreApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.UI
{
    public class AppMenu
    {
        private ConsoleMenu mainMenu = null;
        private ConsoleMenu adminMenu = null;
        private ConsoleMenu sellerMenu = null;


        private Store store;
        private Administrator currentAdmin = null;
        private Seller currentSeller = null;
    

        private void AddProduct(Seller seller)
        {
            Console.Write("Product to add: ");
            var prodId = Console.ReadLine();

            Console.Write("Qty: ");
            var strQty = Console.ReadLine();
            int qty = 0;
            int.TryParse(strQty, out qty);
            try
            {
                seller.AddProductToSell(prodId, qty);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void StartSell(Seller seller)
        {
            Console.WriteLine("Cash register: ");
            var cashReg = Console.ReadLine();
            try
            {
                seller.StartSell(cashReg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ViewReceipt(Seller seller)
        {
            try
            {
                var currentRecipt = seller.GetCurrentReceipt();
                if (currentRecipt == null)
                {
                    Console.WriteLine($"There is no active receipt. Please make sure that you have a sell in progress.");
                    return;
                }

                foreach (var item in currentRecipt.Items)
                {
                    Console.WriteLine($"{item.ProductName} {item.PricePerUnit} RON x {item.Qty} - {item.Total}");
                }

                Console.WriteLine($"--------------\n TOTAL VALUE: {currentRecipt.Total}");
            }
            catch (NoCashRegisterSelectedException e)
            {
                Console.WriteLine($"There is no cash register selected. Seller: {e.CurrentSeller.PersonalData.FirstName} {e.CurrentSeller.PersonalData.LastName} ");
            }
            catch (NoSellInProgressException e)
            {
                Console.WriteLine($"There is no sell in progress for the selected register: {e.Register.Id}");
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected error occured.");                
            }
        }

        private void FinalizeSell(Seller seller)
        {
            ViewReceipt(seller);
            Console.ReadLine();
            try
            {
                seller.CloseSell();
            }
            catch (NoCashRegisterSelectedException e)
            {
                Console.WriteLine($"There is no cash register selected for finalizing a sell. Seller: {e.CurrentSeller.PersonalData.FirstName} {e.CurrentSeller.PersonalData.LastName} ");
            }
            catch (NoSellInProgressException e)
            {
                Console.WriteLine($"We cannot finalize a sell since there is none in progress. {e.Register.Id}");
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected error occured.");
            }
        }
        private void DisplayStock(Stock stockToDisplay)
        {
            Console.WriteLine("---------  STOCK -------------");
            foreach (var item in stockToDisplay.StockItems)
            {
                Console.WriteLine($"{item.Product.Id}\t{item.Product.Name}\t{item.Qty}");
            }

            Console.WriteLine($"-- TOTAL VALUE: {stockToDisplay.TotalValue} RON");
            Console.ReadLine();
        }
        private List<StockItem> GetStandardStock()
        {
            return new List<StockItem>
            {
                new StockItem{
                                Qty = 10,
                                Product = new Product
                                {
                                    Id = "1",
                                    Name = "Prdocut 1",
                                    Description = "Some desc. for prod. 1",
                                    Price = 10.0M
                                }
                },
                new StockItem{
                                Qty = 16,
                                Product = new Product
                                {
                                    Id = "2",
                                    Name = "Prdocut Two",
                                    Description = "Some desc. for prod. 2",
                                    Price = 2.0M
                                }
                },
                new StockItem{
                                Qty = 20,
                                Product = new Product
                                {
                                    Id = "3",
                                    Name = "Prdocut 3",
                                    Description = "Some desc. for prod. 3",
                                    Price = 2.10M
                                }
                }

            };
        }

        private ConsoleMenu GetManageStockMenu()
        {
            return new ConsoleMenu(1, "Stock", new List<BaseMenuItem> {

            });
        }
        private ConsoleMenu GetManageCashRegistersMenu()
        {
            return new ConsoleMenu(2, "CashRegisters", new List<BaseMenuItem>
            {

            });
        }
        private ConsoleMenu GetAdminMenu()
        {
            return new ConsoleMenu(new List<BaseMenuItem>
            {
                GetManageStockMenu(),
                GetManageCashRegistersMenu()
            });
        }

        private ConsoleMenu GetStartSellMenu(Seller seller)
        {
            return new ConsoleMenu(1, "Start sell", new List<BaseMenuItem>
            {
                new ConsoleMenuItem(1, "Start sell", (param)=>{ StartSell(seller); }),
                new ConsoleMenuItem(2, "Add product", (param)=>{ AddProduct(seller); }),
                new ConsoleMenuItem(3, "View receipt", (param)=>{ ViewReceipt(seller); Console.ReadLine(); }),
                new ConsoleMenuItem(4, "Finalize sell", (param)=>{ FinalizeSell(seller); Console.ReadLine(); }),
            });
        }

        private ConsoleMenu GetSellerMenu(Seller seller)
        {
            return new ConsoleMenu(
                    new List<BaseMenuItem>
                    {
                        GetStartSellMenu(seller),
                        new ConsoleMenuItem(2, "View Stock", (param)=>{
                            DisplayStock(store.Stock);
                        })
                    });
        }
        public void Initialize()
        {
            store = new Store();
            store.LoadStock(GetStandardStock());
            store.LoadDefaultCashRegisters();
            currentAdmin = new Administrator(store);
            currentSeller = new Seller(store) 
            { 
                PersonalData = new Person() { FirstName = "SellerOneFirstName", LastName = "SellerOneLastName" }
            };
            adminMenu = GetAdminMenu();
            sellerMenu = GetSellerMenu(currentSeller);

            List<BaseMenuItem> menuItems = new List<BaseMenuItem>()
            {
                new ConsoleMenuItem(1, "Use app. as Admin", (param) => {  adminMenu.Execute(currentAdmin); }),
                new ConsoleMenuItem(2, "Use app as Seller", (param) => {  sellerMenu.Execute(currentSeller); })
            };
            
            mainMenu = new ConsoleMenu(menuItems);
        }

        public void Run()
        {
            mainMenu.Execute(this);
        }

    }
}
