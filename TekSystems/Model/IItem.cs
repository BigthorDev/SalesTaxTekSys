using System.Collections.Generic;

namespace TekSystems.Model
{
    public interface IItem
    {
        int[] AvailableTaxes { get; set; }
        string ItemDescription { get; set; }
        int ItemID { get; set; }
        string ItemName { get; set; }
        double Price { get; set; }

        Dictionary<int, double> GetTotalTaxDetail();
        double GetTotalTaxes();
    }
}