using System.Collections.Generic;

namespace TekSystems.Model
{
    public interface IItem
    {
        int[] AvailableTaxes { get; set; }
        string ItemDescription { get; set; }
        int ItemID { get; set; }
        string ItemName { get; set; }
        decimal Price { get; set; }

        Dictionary<int, decimal> GetTotalTaxDetail();
        decimal GetTotalTaxes();
    }
}