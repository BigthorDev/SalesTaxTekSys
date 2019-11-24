using System;
using System.Collections.Generic;
using System.Text;
using TekSystems.Data;
using TekSystems.Model;
using TekSystems.Utilities;

namespace TekSystems.Service
{
    public class BusinessLogic : IBusinessLogic
    {
        private ILogger _logger;
        private IReceipt _receipt;
        private IDataAccess _dataAccess;
        private IShoppingCart _shoppingCart;

        public BusinessLogic(ILogger logger, IReceipt receipt, IDataAccess dataAccess, IShoppingCart shoppingCart)
        {
            if (logger == null || receipt == null || dataAccess == null || shoppingCart == null)
            {
                throw new ArgumentNullException();
            }
            _logger = logger;
            _receipt = receipt;
            _dataAccess = dataAccess;
            _shoppingCart = shoppingCart;
        }

        public decimal CalculateTotalTaxes(IShoppingCart shoppingCart)
        {
            return 0;
        }

        public void PrintShoppingCart(IShoppingCart shoppingCart)
        {
            Console.WriteLine("");
            if (shoppingCart.CartItems.Count < 1)
                Console.WriteLine("Shopping Cart is empty");
            else
                shoppingCart.CartItems.ForEach(art => Console.WriteLine($"- Article: { art.SelectedItem.ItemName } - Qty: {art.Amount.ToString()}"));

            Console.WriteLine("");
        }

        public IReceipt CheckOut(IShoppingCart shoppingCart)
        {
            Console.WriteLine("\n---------------- RECEIPT ------------------\n");
            Console.WriteLine(">> ARTICLES:");

            CalculateFinalItemsReceipt(shoppingCart);

            Console.WriteLine("\n----------------- RESUME -------------------\n");
            Console.WriteLine($"Price Before Taxes \t {MathRound.MathRoundTwoDecimals(_receipt.FinalPriceBeforeTaxes):C2}");
            Console.WriteLine($"\nTotal Taxes \t\t {_receipt.TotalTaxes:C2}");
            Console.WriteLine($"Total \t\t\t {_receipt.FinalPrice:C2}");

            return _receipt;
        }

        public List<Item> GetAllItemsInStore()
        {            
            return _dataAccess.GetAllItems();
        }

        public IItem GetItemByID(int itemID)
        {
            return _dataAccess.GetItemByID(itemID);
        }

        public List<IShoppingCartItem> AddProductToShoppingCart(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems)
        {
            return _shoppingCart.AddProduct(item, currentCartItems);
        }

        public List<IShoppingCartItem> RemoveProductFromShoppingCart(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems)
        {
            return _shoppingCart.RemoveProduct(item, currentCartItems);
        }

        private void CalculateFinalItemsReceipt(IShoppingCart shoppingCart)
        {
            //decimal totalInTaxes = 0;
            foreach (IShoppingCartItem item in shoppingCart.CartItems)
            {
                var singleItemtaxes = item.SelectedItem.GetTotalTaxDetail();
                decimal totalSingleItemTaxes = item.SelectedItem.GetTotalTaxes();
                decimal itemTotalPriceWithTaxes = (item.SelectedItem.Price + totalSingleItemTaxes) * item.Amount;

                Console.WriteLine($"\n-Item: { item.SelectedItem.ItemName}" +
                                    $"\n\t-Qty: { item.Amount}" +
                                    $"\n\t-Unit Price: { item.SelectedItem.Price:C2}" +
                                    $"\n\t-Total Price with taxes: {Math.Round(itemTotalPriceWithTaxes,2) :C2}");

                //Gets the value of the taxes based on the enum y struct
                foreach (int taxID in singleItemtaxes.Keys)
                {
                    decimal totalTaxByQuantity = MathRound.MathRoundTwoDecimals(item.Amount * singleItemtaxes[taxID]);
                    Console.WriteLine("\t>>> " + Enum.GetName(typeof(TaxTypes), taxID) + " \t= $ " + totalTaxByQuantity.ToString("C2"));
                    //totalInTaxes += totalTaxByQuantity;
                }
                _receipt.FinalPrice += itemTotalPriceWithTaxes;
                _receipt.TotalTaxes += totalSingleItemTaxes;
                _receipt.FinalPriceBeforeTaxes += item.SelectedItem.Price * item.Amount;
            }
        }

      
    }
}
