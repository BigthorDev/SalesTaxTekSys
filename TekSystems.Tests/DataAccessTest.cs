using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TekSystems.Data;
using TekSystems.Model;
using TekSystems.Utilities;

namespace TekSystems.Tests
{
    [TestClass]
    public class DataAccessTest
    {
        static ILogger logger = FactoryTest.CreateLogger();
        [TestMethod]
        public void GetAllArticles()
        {
            IDataAccess dao = FactoryTest.CreateDataAccess();
            var allArticles = dao.GetAllItems();
            Assert.IsNotNull(allArticles);
        }

        [TestMethod]
        public void GetArticleByID()
        {
            IItem expected = FactoryTest.CreateItem();
            expected.ItemID = 1;
            expected.ItemName = "Book";
            expected.Price = 12.49;
            expected.ItemDescription = "This is a book about Tek Systems";
            expected.AvailableTaxes = new int[0];

            IDataAccess dao = FactoryTest.CreateDataAccess();
            var actual = dao.GetItemByID(1);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Price, actual.Price);
        }

        [TestMethod]
        public void NumericValidation()
        {
            bool isNumeric = false;
            do
            {
                int response = -1;
                if (int.TryParse("110b", out int tmpResp))
                {
                    response = tmpResp;
                    isNumeric = true;
                }
                else
                {
                    var test = Console.ReadLine();
                }
            }
            while (!isNumeric);
        }
    }
}
