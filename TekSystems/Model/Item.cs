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
        public double Price { get; set; }
        public int[] AvailableTaxes { get; set; }

        public Item()
        {

        }

        public Item(int itemID, string itemName, string itemDescription, double price, int[] availableTaxes)
        {
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.Price = price;
            this.ItemDescription = itemDescription;
            this.AvailableTaxes = availableTaxes;
        }

        public double GetTotalTaxes()
        {
            Double total = 0.0;
            foreach (var tt in GetTotalTaxDetail())
            {
                total += (double)tt.Value;
            }
            return Math.Round(total, 2);
        }


        public Dictionary<int, double> GetTotalTaxDetail()
        {
            double taxValue = 0;
            Dictionary<int, double> allTaxes = new Dictionary<int, double>();

            // this was with somebody's help that knows reflection.
            foreach (var tax in Enum.GetNames(typeof(TaxTypes)))
            {
                int taxTypeID = (int)Enum.Parse(typeof(TaxTypes), tax);
                if (AvailableTaxes.Contains(taxTypeID))
                {
                    double taxAmount = (double)typeof(TaxValues).GetField(tax).GetValue(new TaxValues());
                    taxValue = Math.Round(this.Price * taxAmount, 2);
                    allTaxes.Add(taxTypeID, taxValue);
                }
            }

            return allTaxes;
        }
    }
}
