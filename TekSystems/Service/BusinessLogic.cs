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

        public double CalculateTotalTaxes(IShoppingCart shoppingCart)
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
            double totalInTaxes = 0;

            foreach (IShoppingCartItem item in shoppingCart.CartItems)
            {
                var taxes = item.SelectedItem.GetTotalTaxDetail();
                Console.WriteLine($"\n-Item: { item.SelectedItem.ItemName}\t-Qty: { item.Amount}\n-Unit Price: $ { item.SelectedItem.Price}\t-Total Price :${ item.SelectedItem.Price * item.Amount}");
                
                //Gets the value of the taxes based on the enum y struct
                foreach (int taxID in taxes.Keys)
                {
                    double totalTaxByQuantity = Math.Round(item.Amount * taxes[taxID], 2);
                    Console.WriteLine(">>> " + Enum.GetName(typeof(TaxTypes), taxID) + " \t= $ " + totalTaxByQuantity);
                    totalInTaxes += totalTaxByQuantity;
                }

                _receipt.FinalPriceBeforeTaxes += item.SelectedItem.Price * item.Amount;
            }
            _receipt.TotalTaxes = Math.Round(totalInTaxes, 2);
            _receipt.FinalPrice = _receipt.FinalPriceBeforeTaxes + _receipt.TotalTaxes;

            Console.WriteLine("\n---------------- RESUME ------------------\n");
            Console.WriteLine($"Price Before Taxes \t$ {Math.Round(_receipt.FinalPriceBeforeTaxes,2)}");
            Console.WriteLine($"Total Taxes \t\t$ {_receipt.TotalTaxes}");
            Console.WriteLine($"\nTotal \t\t\t$ {Math.Round(_receipt.FinalPrice,2)}");

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
      
    }
}
