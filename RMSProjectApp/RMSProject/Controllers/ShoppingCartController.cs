using Microsoft.AspNetCore.Mvc;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;
using static RMSProject.ViewModels.ShoppingCartViewData;

namespace RMSProject.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ViewResult Index()
        {
            var items = _unitOfWork.ShoppingCartRepository.GetShoppingCartItems();
            _unitOfWork.ShoppingCartRepository.ShoppingCartItems = items;




            var shoppingCartViewModel = new ShoppingCartViewData(
                _unitOfWork.ShoppingCartRepository, 
                _unitOfWork.ShoppingCartRepository.GetShoppingCartTotal()
                );

            return View(shoppingCartViewModel);
        }


        public RedirectToActionResult AddToShoppingCart(int menuItemId)
        {
            var selectedMenuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(p => p.Id == menuItemId);

            if (selectedMenuItem != null)
            {
                _unitOfWork.ShoppingCartRepository.AddToCart(selectedMenuItem);
            }
            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoveFromShoppingCart(int menuItemId)
        {
            var selectedMenuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(p => p.Id == menuItemId);

            if (selectedMenuItem != null)
            {
                _unitOfWork.ShoppingCartRepository.RemoveFromCart(selectedMenuItem);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearBasket()
        {
            _unitOfWork.ShoppingCartRepository.ClearCart();

            return RedirectToAction("Index");
        }
    }
}
