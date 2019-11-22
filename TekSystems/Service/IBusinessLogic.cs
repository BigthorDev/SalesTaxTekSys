using System.Collections.Generic;
using TekSystems.Model;

namespace TekSystems.Service
{
    public interface IBusinessLogic
    {
        double CalculateTotalTaxes(IShoppingCart shoppingCart);
        IReceipt CheckOut(IShoppingCart shoppingCart);
        void PrintShoppingCart(IShoppingCart shoppingCart);
        List<Item> GetAllItemsInStore();

        IItem GetItemByID(int itemID);

        List<IShoppingCartItem> AddProductToShoppingCart(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems);

        List<IShoppingCartItem> RemoveProductFromShoppingCart(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems);
    }
}