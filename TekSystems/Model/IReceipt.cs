namespace TekSystems.Model
{
    public interface IReceipt
    {
        decimal FinalPrice { get; set; }
        decimal FinalPriceBeforeTaxes { get; set; }
        IShoppingCartItem[] SelectedItems { get; set; }
        int TotalItems { get; set; }
        decimal TotalTaxes { get; set; }
    }
}