using Microsoft.EntityFrameworkCore;
using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using System.IO.Pipelines;

namespace RMSProject.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCartRepository>, IShoppingCartRepository
    {
        private readonly ModelsDbContext _context;

        public ShoppingCartRepository(ModelsDbContext dbContext) :base(dbContext)
        {
            _context = dbContext;
        }

        // The id of the shopping cart / the user this shopping cart is assoicated with
        public string? ShoppingCartId { get; set; }

        // The shopping cart that is going to display a list of shopping cart items
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;



        /// <summary>
        /// Returns a shopping cart item for individual users based on session information.
        /// It does this by getting session information to see if the user already has an existing cart
        /// If not, we create a new cart for them.
        /// </summary>
        /// <param name="services">Injecting the services collection to get information about current user.</param>
        /// <returns>A shopping cart.</returns>
        /// <exception cref="Exception"></exception>
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            // Get the user session data. From this will get or set session cookies
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            // Gets the model db context 
            ModelsDbContext context = services.GetService<ModelsDbContext>() ?? throw new Exception("Error initializing");

            // If the user has visited our site before, they will have a CartId key, we can just use this to retrieve their previous basket.
            // If the user has NOT visited our site before, then we will generate a new key value pair for the user.
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            // We set the CartID key to either the previous session information or a new one, based on previous interactions
            session?.SetString("CartId", cartId);

            // return the shopping cart
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// Adds the specified MenuItem to the shopping cart.
        /// </summary>
        /// <param name="menuItem">The MenuItem to add to the users cart.</param>
        public void AddToCart(MenuItem menuItem)
        {
            // First we check to see if the shopping cart already contains the same MenuItem the user is trying to add for the same user/shopping cart.
            // I.e. Before adding to the cart, we see if the same item is already in the cart
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.MenuItem.Id == menuItem.Id && s.ShoppingCartId == ShoppingCartId);

            // If it isn't, then we add one of these menuItems to the cart, and set the cart/user its associated with
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    MenuItem = menuItem,
                    Amount = 1
                };

                // As this is a new item, we call the add function
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                // If the same menuItem is already in the cart, then we just need to increase the amount.
                shoppingCartItem.Amount++;
            }
            // Save the changes made to the dbContext/Shopping cart.
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes all items from the shopping cart.
        /// </summary>
        public void ClearCart()
        {
            // For each item in the shopping cart, if they have the same shopping cart Id as the one we want to clear, we remove it.
            var cartItems = _context.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            // remove all the items in the cart
            _context.ShoppingCartItems.RemoveRange(cartItems);

            // Update dbContext to show changes 
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves a list of all the MenuItems the user has in their basket/cart.
        /// </summary>
        /// <returns>A list of each item in the cart.</returns>
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // Gets all the items that are associated with a specific cart/user. We also make sure to return the MenuItem as well
            return ShoppingCartItems ??= _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.MenuItem)
                .ToList();
        }

        /// <summary>
        /// Gets the total of amount of the shopping cart.
        /// </summary>
        /// <returns>The total value.</returns>
        public decimal GetShoppingCartTotal()
        {
            // go through each item in the current shopping cart and sum the (amount*ItemPrice)
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.MenuItem.ItemPrice * c.Amount).Sum();
            return total;
        }


        /// <summary>
        /// Remove a specific menuItem from the cart.
        /// </summary>
        /// <param name="menuItem">The item to remove.</param>
        /// <returns>The number of items remaining in the cart for that specific item.</returns>
        public int RemoveFromCart(MenuItem menuItem)
        {
            // Find the shopping cart item we want to remove
            var shoppingCartItem =
                   _context.ShoppingCartItems.SingleOrDefault(
                       s => s.MenuItem.Id == menuItem.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            // If the item we want to delete is in the shopping cart
            if (shoppingCartItem != null)
            {
                // And if there is more than one of those items in the shopping cart
                if (shoppingCartItem.Amount > 1)
                {
                    // we reduce the number of items we have by one
                    shoppingCartItem.Amount--;
                    // Update the quantity we have of the specific menuItem
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    // if there is ONLY 1 item, then we just remove it entirely
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            // save changes
            _context.SaveChanges();

            // the quanitity of the specific menuItem we have left in the shopping cart
            return localAmount;
        }
    }
}
