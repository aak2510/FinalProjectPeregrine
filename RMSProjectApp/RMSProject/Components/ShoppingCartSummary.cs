using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;

namespace RMSProject.Components
{
    public class ShoppingCartSummary : ViewComponent
    {

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartSummary(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            var items = _unitOfWork.ShoppingCartRepository.GetShoppingCartItems();
            _unitOfWork.ShoppingCartRepository.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewData(_unitOfWork.ShoppingCartRepository, _unitOfWork.ShoppingCartRepository.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

    }
}
