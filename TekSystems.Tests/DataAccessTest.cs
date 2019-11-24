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
            expected.Price = 12.49m;
            expected.ItemDescription = "This is a book about Tek Systems";
            expected.AvailableTaxes = new int[0];

            IDataAccess dao = FactoryTest.CreateDataAccess();
            var actual = dao.GetItemByID(1);

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Price, actual.Price);
        }

        /// <summary>
        /// Validation Test
        /// </summary>
        [TestMethod]
        public void NumericValidation()
        {
            bool isNumeric = false;
            var input = "110b";
            int response = -1;

            if (int.TryParse(input, out int tmpResp))
            {
                response = tmpResp;
                isNumeric = true;
            }

            Assert.IsTrue(isNumeric);
        }
    }
}
