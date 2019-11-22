namespace TekSystems.Model
{
    public interface IShoppingCartItem
    {
        int Amount { get; set; }
        IItem SelectedItem { get; set; }
    }
}