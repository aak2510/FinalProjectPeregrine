using System.IO.Pipelines;

namespace RMSProject.Models
{
    /// <summary>
    /// This call will be used to represent each individual menu item that will be added to a list for each order placed.
    /// Each one of these items is associated with an item in the basket.
    /// </summary>
    public class OrderItemInBasket
    {
        public int OrderItemInBasketId { get; set; }

        // The order associated with these details - Foreign Key
        public int OrderId { get; set; }

        // The menu item ordered - Foreign key
        public int MenuItemId { get; set; }

        // The number of menu items ordered
        public int Amount { get; set; }

        // the price of the menu item
        public decimal Price { get; set; }

        // Navigational properties
        public MenuItem MenuItem { get; set; } = default!;
        public OrderInformation Order { get; set; } = default!;
    }
}