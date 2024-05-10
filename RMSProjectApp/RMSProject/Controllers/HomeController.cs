using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;
using System.Diagnostics;
using System.IO.Pipelines;

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
            // If the user logged in is the admin, then return the admin view
            // Add in role based implementation 
            if (User.Identity.Name == "admin@admin.com")
            {
                // return the specific view -- to fix
                return View("Views/Home/StaffHome/Index.cshtml");
            }
      
            return View();
        }

        public IActionResult Menu(string? typeOfMeal)
        {
            IEnumerable<MenuItem> menuItems;

            if (string.IsNullOrEmpty(typeOfMeal))
            {
                menuItems = _unitOfWork.MenuItemsRepository.GetAll();
            }
            else
            {
                menuItems = _unitOfWork.MenuItemsRepository.GetAll().Where(p => p.TypeOfMeal == Enum.Parse<MealType>(typeOfMeal));

            }
            ListViewData vm = new ListViewData();
            vm.MenuItems = menuItems;
            vm.NutritionalInformation = _unitOfWork.NutritionalInformationRepository.GetAll();

            return View(vm);
        }

        // GET: MenuItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(m => m.Id == id);

            var nutritionalInfo = _unitOfWork.NutritionalInformationRepository.GetFirstOrDefault(m => m.MenuItemId == id);


            if (menuItem == null || nutritionalInfo == null)
            {
                return NotFound();
            }

            // Add allegern information into view bag to display
            ViewBag.HasNuts = nutritionalInfo.HasNuts;
            ViewBag.IsVegetarian = nutritionalInfo.IsVegetarian;
            ViewBag.IsVegan = nutritionalInfo.IsVegan;


            // create view model
            MenuItemsViewData vm = new MenuItemsViewData();
            vm.MenuItem = menuItem;
            vm.NutritionalInformation = nutritionalInfo;



            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
