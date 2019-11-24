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
        public decimal FinalPrice { get; set; }
        public decimal FinalPriceBeforeTaxes { get; set; }
        public decimal TotalTaxes { get; set; }
    }
}
