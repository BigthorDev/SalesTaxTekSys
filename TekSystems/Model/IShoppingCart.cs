using System.Collections.Generic;

namespace TekSystems.Model
{
    public interface IShoppingCart
    {
        List<IShoppingCartItem> CartItems { get; set; }
        void Print(IShoppingCart shoppingCart);
        List<IShoppingCartItem> RemoveProduct(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems);
        List<IShoppingCartItem> AddProduct(IShoppingCartItem item, List<IShoppingCartItem> currentCartItems);
    }
}