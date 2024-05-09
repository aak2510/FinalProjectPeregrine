using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
