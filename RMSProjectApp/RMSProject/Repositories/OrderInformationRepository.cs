using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;

namespace RMSProject.Repositories
{
    public class OrderInformationRepository : Repository<OrderInformation>, IOrderInformationRepository
    {

        private ModelsDbContext _context;
        private readonly IShoppingCartRepository _shoppingCart;

        public OrderInformationRepository(ModelsDbContext dbContext, IShoppingCartRepository shoppingCart) : base(dbContext)
        {
            _context = dbContext;
            _shoppingCart = shoppingCart;
        }


        public void Create(OrderInformation order)
        {
            order.OrderPlacedTimeAndDate = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderItemInBasket>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderItemInBasket
                {
                    Amount = shoppingCartItem.Amount,
                    MenuItemId = shoppingCartItem.MenuItem.Id,
                    Price = shoppingCartItem.MenuItem.ItemPrice
                };

                //remove and call separately - unit of work
                order.OrderDetails.Add(orderDetail);
            }


            //remove and call separately - unit of work
            _context.OrderInformation.Add(order);
            _context.SaveChanges();

        }
    }
}
