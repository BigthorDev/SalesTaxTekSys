using System;
using System.Collections.Generic;
using System.Text;

namespace TekSystems.Model
{
    public class ShoppingCartItem : IShoppingCartItem
    {
        public IItem SelectedItem { get; set; }
        public int Amount { get; set; }
    }
}
