﻿using System;
using System.Collections.Generic;
using TekSystems.Model;
using TekSystems.Service;
using TekSystems.Utilities;

namespace TekSystems.Main
{
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
            Console.Write("Welcome to TEK Store\nIn this task there are 2 different options, you can select predefined inputs or you can build your own shopping cart.\n");
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
                case "predefinedinput":
                    blo = Factory.CreateBusinessLogic();
                    Console.WriteLine("\nPlease insert the predefined Input ID you want to check (1 to 3)");
                    int listId = validator.ValidateListIDValue(Console.ReadLine());
                    ProcessPredefinedList(listId);
                    ShoppingCart = Factory.CreateShoppingCart();
                    break;
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
                    break;
                case "clear":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command\n");
                    break;
            }
        }

        private static void ShowInfo()
        {
            Console.WriteLine("- predefinedInput: Generates a shopping cart based on the coding exercise document info.");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine("- info           : Shows all the different calls the user can make.");
            Console.WriteLine("- showArticles   : Shows all the articles in the system.");
            Console.WriteLine("- addArticle     : Method for adding Articles in ShoppingCart.");
            Console.WriteLine("- removeArticle  : Method for removing Articles from the ShoppingCart.");
            Console.WriteLine("- shoppingCart   : Shows the articles in the shopping cart.");
            Console.WriteLine("- checkout       : Finalize shopping process and generates the receipt.");
            Console.WriteLine("- clear          : Clear console.");
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