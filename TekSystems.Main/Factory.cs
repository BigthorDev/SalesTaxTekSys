using System;
using System.Collections.Generic;
using System.Text;
using TekSystems.Data;
using TekSystems.Service;
using TekSystems.Model;
using TekSystems.Utilities;

namespace TekSystems.Main
{
    public class Factory
    {
        public static IItem CreateItem()
        {
            return new Item();
        }

        public static IShoppingCartItem CreateShoppingCartItem()
        {
            return new ShoppingCartItem();
        }

        public static IShoppingCart CreateShoppingCart()
        {
            ShoppingCart tmp = new ShoppingCart(CreateShoppingCartItem(), CreateLogger());
            tmp.CartItems = new List<IShoppingCartItem>();
            return tmp;
        }

        public static IReceipt CreateReceipt()
        {
            return new Receipt();
        }

        public static ILogger CreateLogger()
        {
            return new Logger();
        }

        public static IDataAccess CreateDataAccess()
        {
            return new DataAccess(CreateItem());
        }

        public static IBusinessLogic CreateBusinessLogic()
        {
            return new BusinessLogic(CreateLogger(), CreateReceipt(),CreateDataAccess(),CreateShoppingCart());
        }

        public static IValidations CreateValidations()
        {
            return new Validations(CreateLogger());
        }
    }
}
