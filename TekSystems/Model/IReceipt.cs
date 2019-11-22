namespace TekSystems.Model
{
    public interface IReceipt
    {
        double FinalPrice { get; set; }
        double FinalPriceBeforeTaxes { get; set; }
        IShoppingCartItem[] SelectedItems { get; set; }
        int TotalItems { get; set; }
        double TotalTaxes { get; set; }
    }
}