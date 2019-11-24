using System;
using System.Collections.Generic;
using TekSystems.Model;
using TekSystems.Service;
using TekSystems.Utilities;

namespace TekSystems.Main
{
    /// <summary>
    /// SalesTax Coding Exercise - .Net and Java - Tek Systems 
    /// </summary>
    class Program
    {
        #region Properties and Variables
        static IShoppingCart ShoppingCart { get; set; }
        static List<Item> AllArticles { get; set; }
        static ILogger Logger = Factory.CreateLogger();
        static IValidations validator = Factory.CreateValidations();
        static IBusinessLogic blo = Factory.CreateBusinessLogic();
        #endregion

        static void Main(string[] args)
        {
            ShoppingCart = Factory.CreateShoppingCart();
            //Print main info in console
            Console.Write("Welcome to TEK Store\n\nIn this application you can select 3 predefined shopping carts (inputs) and the receipts (output) will be calculated automatically. \n");
            Console.WriteLine("");            
            ShowInfo();
            try
            {
                //Gets all the items from the 'DB'
                AllArticles = blo.GetAllItemsInStore();
                do
                {
                    //Gets the action the user wants to execute.
                    string clientEntry = Console.ReadLine();
                    string ActionCall = clientEntry.Split(' ')[0];
                    if (ActionCall == "exit") return;
                    ExecuteActionCall(ActionCall.ToLower());
                }
                while (true);

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Logger.Log(ex.Message);
                Console.ReadKey();
            }
        }

        #region Interface Interaction

        private static void ExecuteActionCall(string actionCall)
        {
            switch (actionCall)
            {
                case "info":
                    ShowInfo();
                    break;
                case "pinput":
                    blo = Factory.CreateBusinessLogic();
                    Console.WriteLine("\nPlease insert the predefined Input ID you want to check (1 to 3)");
                    int listId = validator.ValidateListIDValue(Console.ReadLine());
                    ProcessPredefinedList(listId);
                    ShoppingCart = Factory.CreateShoppingCart();
                    break;
                #region Uncomment for Custom Shopping Cart
                    /*
                case "showarticles":
                    ShowArticlesInStore();
                    break;
                case "addarticle":
                    Console.WriteLine("\nPlease insert the Article ID you want to buy");
                    int itemID = validator.ValidateNumericValue(Console.ReadLine());
                    AddArticle(itemID, false);
                    break;
                case "removearticle":
                    Console.WriteLine("\nPlease insert the ID of the article you want to delete");
                    int articleID = validator.ValidateNumericValue(Console.ReadLine());
                    RemoveArticle(articleID);
                    break;
                case "shoppingcart":
                    blo.PrintShoppingCart(ShoppingCart);
                    break;
                case "checkout":
                    blo.CheckOut(ShoppingCart);
                    ShoppingCart = Factory.CreateShoppingCart();
                    break;*/
                #endregion
                case "clear":
                    ShoppingCart = Factory.CreateShoppingCart();
                    Console.Clear();
                    ShowInfo();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command\n");
                    break;
            }
        }

        private static void ShowInfo()
        {
            Console.WriteLine("- pInput         : Generates a shopping cart based on the coding exercise document info.");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine("- info           : Shows all the different actions the user can make.");
            #region Uncomment for Custom Shopping Cart
            //Console.WriteLine("- showArticles   : Shows all the articles in the system.");
            //Console.WriteLine("- addArticle     : Method for adding Articles in ShoppingCart.");
            //Console.WriteLine("- removeArticle  : Method for removing Articles from the ShoppingCart.");
            //Console.WriteLine("- shoppingCart   : Shows the articles in the shopping cart.");
            //Console.WriteLine("- checkout       : Finalize shopping process and generates the receipt.");
            #endregion
            Console.WriteLine("- clear          : Clear console and shopping cart.");
            Console.WriteLine("- exit           : End process.\n");
        }

        private static void ShowArticlesInStore()
        {
            Console.WriteLine("");
            AllArticles.ForEach(x => Console.WriteLine($"- ID:{ x.ItemID } -Name: { x.ItemName } (${x.Price})"));
            Console.WriteLine("");
        }
        #endregion

        #region Predefined Lists

        public static void ProcessPredefinedList(int listID)
        {
            int[] ids = new int[] { };
            switch (listID)
            {
                case 1:
                    ids = new int[] { 1, 2, 3 };
                    blo.CheckOut(GetPredefinedCart(ids));
                    break;
                case 2:
                    ids = new int[] { 4, 7 };
                    blo.CheckOut(GetPredefinedCart(ids));
                    break;
                case 3:
                    ids = new int[] { 8, 6, 9, 5 };
                    blo.CheckOut(GetPredefinedCart(ids));
                    break;
                default:
                    break;
            }
        }

        private static IShoppingCart GetPredefinedCart(int[] itemIDs)
        {
            List<IShoppingCartItem> iscLst = new List<IShoppingCartItem>();
            foreach (int i in itemIDs)
            {
                IItem item = blo.GetItemByID(i);
                IShoppingCartItem tt = Factory.CreateShoppingCartItem();
                tt.Amount = 1;
                tt.SelectedItem = item;
                iscLst.Add(tt);
            }
            ShoppingCart.CartItems = iscLst;
            return ShoppingCart;
        }
        #endregion


        /*
         * I CREATED THIS SECTION FOR HAVING CUSTOM INPUT OPTION. I KNOW THE DOCUMENT SAYS IT IS NOT NECESARY BUT I WANTED TO SHOW THE APP HAS AN EXPANDABLE DESIGN.
         */
        #region BLO interaction

        private static void AddArticle(int itemID, bool isCustomList)
        {
            int amount;
            Console.WriteLine("\nPlease insert the quanity");
            amount = validator.ValidateNumericValue(Console.ReadLine());
            SaveProductInShoppingCart(itemID, amount);
            string finalNotificationAmount = amount > 1 ? "have" : "has";
            Logger.Log($"{ amount } {AllArticles.FindLast(art => art.ItemID == itemID).ItemName } {finalNotificationAmount} been assigned to the cart\n");
        }

        private static void SaveProductInShoppingCart(int articleID, int numberOfArticles)
        {
            IShoppingCartItem sc = BuildCartItem(articleID, numberOfArticles);
            ShoppingCart.CartItems = blo.AddProductToShoppingCart(sc,ShoppingCart.CartItems);
        }

        private static void RemoveArticle(int articleID)
        {
            int amount;
            Console.WriteLine("\nPlease insert the quanity");
            amount = validator.ValidateNumericValue(Console.ReadLine());
            RemoveProductFromShoppingCart(articleID, amount);
            Logger.Log("\nShopping cart has been updated");
        }

        private static void RemoveProductFromShoppingCart(int articleID, int numberOfArticles)
        {
            IShoppingCartItem sc = BuildCartItem(articleID, numberOfArticles);
            ShoppingCart.CartItems = blo.RemoveProductFromShoppingCart(sc, ShoppingCart.CartItems);
        }

        private static IShoppingCartItem BuildCartItem(int itemID, int numberOfItems)
        {
            IShoppingCartItem sc = Factory.CreateShoppingCartItem();
            try
            {                
                IItem tempItem = blo.GetItemByID(itemID);
                sc.SelectedItem = tempItem;
                sc.Amount = numberOfItems;
            }
            catch 
            {
                throw new Exception("There was a problem Building the shopping cart item");
            }

            return sc;
        }

        #endregion



    }
}
