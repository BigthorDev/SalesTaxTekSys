using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TekSystems.Data;
using TekSystems.Model;
using TekSystems.Service;
using TekSystems.Utilities;

namespace TekSystems.Tests
{
    [TestClass]
    public class BusinessLogicTest
    {

        static IDataAccess dao = FactoryTest.CreateDataAccess();
        static ILogger logger = FactoryTest.CreateLogger();
        static IReceipt receipt = FactoryTest.CreateReceipt();
        static IShoppingCart cart = FactoryTest.CreateShoppingCart();
        static IBusinessLogic blo = FactoryTest.CreateBusinessLogic();

        [TestMethod]
        public void CalculateItemTotalTaxes()
        {
            var allArticles = dao.GetAllItems();
            decimal expected = MathRound.MathRoundTwoDecimals(allArticles[7].Price * (TaxValues.IMPORTED_TAX + TaxValues.SALES_TAX));
            decimal actual = allArticles[7].GetTotalTaxes();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckOut()
        {
            // ---- Functional Test
            var allArticles = dao.GetAllItems();
            IShoppingCartItem item = FactoryTest.CreateShoppingCartItem();
            item.SelectedItem = allArticles[0];
            item.Amount = 4;

            IShoppingCartItem item2 = FactoryTest.CreateShoppingCartItem();
            item2.SelectedItem = allArticles[1];
            item2.Amount = 3;
            cart.CartItems.Add(item);
            cart.CartItems.Add(item2);
            blo.CheckOut(cart);

        }
    }
}
