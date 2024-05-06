using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;
using System.Diagnostics;

namespace RMSProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            // Nutrional information has a Navigational Property to access MenuItems, so we will use that to display information
            IEnumerable<MenuItem> menuItems = _unitOfWork.MenuItemsRepository.GetAll();
     
            
            return View(menuItems);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
