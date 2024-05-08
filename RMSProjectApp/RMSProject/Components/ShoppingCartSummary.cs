using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;

namespace RMSProject.Components
{
    public class ShoppingCartSummary : ViewComponent
    {

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartSummary(IUnitOfWork unitOfWorkd)
        {
            _unitOfWork = unitOfWorkd;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };

            //var items = _shoppingCart.GetShoppingCartItems();
            _unitOfWork.ShoppingCartRepository.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewData(_unitOfWork.ShoppingCartRepository, _unitOfWork.ShoppingCartRepository.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

    }
}
