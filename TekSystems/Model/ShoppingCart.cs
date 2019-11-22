using System;
using System.Collections.Generic;
using System.Text;
using TekSystems.Utilities;

namespace TekSystems.Model
{
    public class ShoppingCart : IShoppingCart
    {
        private IShoppingCartItem _shoppingCartItem;
        private readonly ILogger _logger;

        public ShoppingCart(IShoppingCartItem shoppingCartItem, ILogger logger)
        {
            if (shoppingCartItem == null || logger == null)
            {
                throw new ArgumentNullException();
            }
            this._shoppingCartItem = shoppingCartItem;
            this._logger = logger;
        }

        public List<IShoppingCartItem> CartItems { get; set; }

        public List<IShoppingCartItem> AddProduct(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems)
        {
            IShoppingCartItem tempItem = currentCartItems.FindLast(p => p.SelectedItem.ItemID == item.SelectedItem.ItemID);
            if (tempItem == null)
            {
                currentCartItems.Add(item);
            }
            else
            {
                currentCartItems.Remove(tempItem);
                tempItem.Amount = tempItem.Amount + item.Amount;
                currentCartItems.Add(tempItem);
            }
            return currentCartItems;
        }

        public List<IShoppingCartItem> RemoveProduct(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems)
        {
            IShoppingCartItem tempItem = currentCartItems.FindLast(p => p.SelectedItem.ItemID == item.SelectedItem.ItemID);
            if (tempItem == null)
            {
                _logger.Log("Selected article is not in the shopping cart \n");
            }
            else
            {
                currentCartItems.Remove(tempItem);
                if (tempItem.Amount > item.Amount)
                {
                    tempItem.Amount = tempItem.Amount - item.Amount;
                    currentCartItems.Add(tempItem);
                }
            }
            return currentCartItems;
        }
    
    }
}
