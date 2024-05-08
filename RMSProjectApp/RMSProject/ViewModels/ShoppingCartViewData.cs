using RMSProject.Repositories.IRepository;

namespace RMSProject.ViewModels
{
    public class ShoppingCartViewData
    {



        public IShoppingCartRepository ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; }

        public ShoppingCartViewData(IShoppingCartRepository shoppingCart, decimal shoppingCartTotal)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }


    }
}