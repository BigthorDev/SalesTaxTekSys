using System;
using System.Collections.Generic;
using System.Text;

namespace TekSystems.Model
{
    public class Receipt : IReceipt
    {
        protected int ReceiptID { get; set; }
        public IShoppingCartItem[] SelectedItems { get; set; }
        public int TotalItems { get; set; }
        public double FinalPrice { get; set; }
        public double FinalPriceBeforeTaxes { get; set; }
        public double TotalTaxes { get; set; }
    }
}
