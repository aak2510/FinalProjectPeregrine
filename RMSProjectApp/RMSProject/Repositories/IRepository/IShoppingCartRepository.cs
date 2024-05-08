using RMSProject.Models;

namespace RMSProject.Repositories.IRepository
{
    // Common methods to be used when interacting with the shopping cart.
    public interface IShoppingCartRepository : IRepository<ShoppingCartRepository>
    {
        void AddToCart(MenuItem menuItem);

        int RemoveFromCart(MenuItem menuItem);

        List<ShoppingCartItem> GetShoppingCartItems();

        void ClearCart();

        decimal GetShoppingCartTotal();

        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
