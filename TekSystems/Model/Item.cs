using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TekSystems.Utilities;

namespace TekSystems.Model
{
    public class Item : IItem
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Price { get; set; }
        public int[] AvailableTaxes { get; set; }

        public Item()
        {

        }

        public Item(int itemID, string itemName, string itemDescription, decimal price, int[] availableTaxes)
        {
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.Price = price;
            this.ItemDescription = itemDescription;
            this.AvailableTaxes = availableTaxes;
        }

        public decimal GetTotalTaxes()
        {
            decimal total = 0;
            foreach (var tt in GetTotalTaxDetail())
            {
                total += (decimal)tt.Value;
            }
            return MathRound.MathRoundTwoDecimals(total);
        }


        public Dictionary<int, decimal> GetTotalTaxDetail()
        {
            decimal taxValue = 0;
            Dictionary<int, decimal> allTaxes = new Dictionary<int, decimal>();

            foreach (var tax in Enum.GetNames(typeof(TaxTypes)))
            {
                int taxTypeID = (int)Enum.Parse(typeof(TaxTypes), tax);
                if (AvailableTaxes.Contains(taxTypeID))
                {
                    decimal taxAmount = (decimal)typeof(TaxValues).GetField(tax).GetValue(new TaxValues());
                    taxValue = this.Price * taxAmount;
                    allTaxes.Add(taxTypeID, taxValue);
                }
            }

            return allTaxes;
        }
    }
}
