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

        //public IActionResult Index()
        //{

        //    // Nutrional information has a Navigational Property to access MenuItems, so we will use that to display information
        //    IEnumerable<MenuItem> menuItems = _unitOfWork.MenuItemsRepository.GetAll();
        //    IQueryable<NutritionalInformation> nutInfo = _unitOfWork.NutritionalInformationRepository.GetAll();
        //    ListViewData vm = new ListViewData();
        //    vm.MenuItems = menuItems;
        //    vm.NutritionalInformation = nutInfo;

        //    return View(vm);
        //}

        public IActionResult Index(MealType typeOfMeal)
        {
            IEnumerable<MenuItem> menuItems;
            string? currentMealTypeDisplay;

            if (string.IsNullOrEmpty(typeOfMeal.ToString()))
            {
                menuItems = _unitOfWork.MenuItemsRepository.GetAll().OrderBy(p => p.Id);
                currentMealTypeDisplay = "All dishes";
            }
            else
            {
                menuItems = _unitOfWork.MenuItemsRepository.GetAll().Where(p => p.TypeOfMeal == typeOfMeal).OrderBy(p => p.Id);
                currentMealTypeDisplay = _unitOfWork.MenuItemsRepository.GetAll().FirstOrDefault(c => c.TypeOfMeal == typeOfMeal)?.TypeOfMeal.ToString();
            }
            ListViewData vm = new ListViewData();
            vm.MenuItems = menuItems;
            vm.NutritionalInformation = _unitOfWork.NutritionalInformationRepository.GetAll();
            vm.currentMealTypeDisplay = currentMealTypeDisplay;
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
