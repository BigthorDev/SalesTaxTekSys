using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekSystems.Data;
using TekSystems.Model;
using TekSystems.Service;
using TekSystems.Utilities;

namespace TekSystems.Tests
{
    public class FactoryTest
    {
        public static IItem CreateItem()
        {
            return new Item();
        }

        public static IShoppingCartItem CreateShoppingCartItem()
        {
            return new ShoppingCartItem();
        }

        public static IReceipt CreateReceipt()
        {
            return new Receipt();
        }

        public static ILogger CreateLogger()
        {
            return new Logger();
        }

        public static IShoppingCart CreateShoppingCart()
        {
            return new ShoppingCart(CreateShoppingCartItem(), CreateLogger());
        }

        public static IDataAccess CreateDataAccess()
        {
            return new DataAccess(CreateItem());
        }

        public static IBusinessLogic CreateBusinessLogic()
        {
            return new BusinessLogic(CreateLogger(), CreateReceipt(), CreateDataAccess(), CreateShoppingCart());
        }

        public static IValidations CreateValidations()
        {
            return new Validations(CreateLogger());
        }
    }
}
