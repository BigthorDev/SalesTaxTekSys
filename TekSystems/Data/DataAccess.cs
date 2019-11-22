using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TekSystems.Model;

namespace TekSystems.Data
{
    public class DataAccess : IDataAccess
    {
        private IItem _Item;
        public DataAccess(IItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _Item = item;
        }

        public List<Item> GetAllItems()
        {
            //I'm using mock data taken from a json file simulating a WebService call or any other data gathering mode that could be used.
            string allText = File.ReadAllText("Data\\articles.json");
            var articlesList = JsonConvert.DeserializeObject<List<Item>>(allText);
            return articlesList;
        }

        public IItem GetItemByID(int ItemID)
        {
            _Item = GetAllItems().Find(x => x.ItemID == ItemID);
            return _Item;
        }
    }


}
