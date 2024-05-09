using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;

namespace RMSProject.Controllers
{
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET
        public IActionResult Checkout()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Checkout(OrderInformation orderInformation)
        {
            if (orderInformation == null)
            {
                return View("Error");
            }

            // Retrieve the list of items associated with the current user and store then in a list
            var items = _unitOfWork.ShoppingCartRepository.GetShoppingCartItems();
            _unitOfWork.ShoppingCartRepository.ShoppingCartItems = items;


            // Have this and the HTML disabled element, in case someone tries to remove the HTML attribute from dev tools.
            // This will cause the model state in the next conditional statement to be false.
            if (_unitOfWork.ShoppingCartRepository.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Please add items into you basket first");
            }


            // If the order information is valid we create an order
            if (ModelState.IsValid)
            {
                // TODO: Change to call this, once the payment has gone through
                _unitOfWork.OrderInformationRepository.Create(orderInformation);
                // Assuming the payment has gone through, we can clear the cart 
                _unitOfWork.ShoppingCartRepository.ClearCart();
                // redirect user to the success page
                return RedirectToAction("CheckoutComplete");
            }

            // if the order wasn't sucessful, we return the user to the checkout page with the viewbag error message
            TempData["CheckoutFail"] = "Ooops, looks like something went wrong there.";
            return View(orderInformation);
        }

        // GET
        public IActionResult CheckoutComplete()
        {



            return View();
        }
    }
}
