using System.IO.Pipelines;

namespace RMSProject.Models
{
    /* 
     * These are the items that will be stored within the users shopping cart.
     * Each shopping cart will consist of a MenuItem, the number of those items
     * and the ShoppingCart/UserId that is associated with that cart, that we 
     * obtain from session information.
     */
    public class ShoppingCartItem
    {
        // The Primary key of the database item.
        public int ShoppingCartItemId { get; set; }

        // The MenuItem being added.
        public MenuItem MenuItem { get; set; } = default!;

        // The amount of the MenuItem being added into the cart.
        public int Amount { get; set; }

        // The shopping cart/user the item is associate with
        public string? ShoppingCartId { get; set; }
    }
}
