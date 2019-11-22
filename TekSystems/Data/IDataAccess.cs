using System.Collections.Generic;
using TekSystems.Model;

namespace TekSystems.Data
{
    public interface IDataAccess
    {
        List<Item> GetAllItems();
        IItem GetItemByID(int ItemID);
    }
}