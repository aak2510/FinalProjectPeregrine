using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using RMSProject.Data;
using RMSProject.Models;
using RMSProject.Repositories;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;

namespace RMSProject.Controllers
{
    public class MenuItemsController : Controller
    {
        // Dependency Injection (DI) for the repositories being used
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MenuItemsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        // Don't need the HttpPost attribute because the method isn't changing the state of the app, just filtering data.
        // GET: MenuItems
        public async Task<IActionResult> Index(string? searchString, string sortByKey)
        {
            /*
             * used to allow for both ascennding and decending order of sorting
             */
            ViewData["ItemName"] = String.IsNullOrEmpty(sortByKey) ? "ItemName_desc" : "";
            ViewData["ItemPrice"] = sortByKey == "ItemPrice" ? "ItemPrice_desc" : "ItemPrice";
            ViewData["TypeOfMeal"] = sortByKey == "TypeOfMeal" ? "TypeOfMeal_desc" : "TypeOfMeal";


            // Get all the movies using repository 
            var menuItems = _unitOfWork.MenuItemsRepository.GetAll();

            // If there are no items, then return objectresult response for problem.
            if (menuItems == null)
            {
                return Problem("Menu Item Entitiy set is null.");
            }

            // If there is information that is submitted in the search field
            if (!String.IsNullOrEmpty(searchString))
            {
                // return all the items which contains the item name specified in the searchString
                menuItems = menuItems.Where(s => s.ItemName!.Contains(searchString));
            }


            switch (sortByKey)
            {
                case "ItemName_desc":
                    menuItems = menuItems.OrderByDescending(s => s.ItemName);
                    break;
                case "ItemPrice_desc":
                    menuItems = menuItems.OrderByDescending(s => s.ItemPrice);
                    break;
                case "TypeOfMeal_desc":
                    menuItems = menuItems.OrderByDescending(s => s.TypeOfMeal);
                    break;
                case "ItemPrice":
                    menuItems = menuItems.OrderBy(s => s.ItemPrice);
                    break;
                case "TypeOfMeal":
                    menuItems = menuItems.OrderBy(S => S.TypeOfMeal);
                    break;
                default:
                    menuItems = menuItems.OrderBy(s => s.ItemName);
                    break;


            }

            // Return the result as a list
            return View(await menuItems.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(m => m.Id == id);

            var nutritionalInfo = _unitOfWork.NutritionalInformationRepository.GetFirstOrDefault(m => m.MenuItemId == id);

            // Todo: Use view models where possible and remember DI (Interfaces methods etc).


            if (menuItem == null || nutritionalInfo == null)
            {
                return NotFound();
            }

            MenuItemsViewData vm = new MenuItemsViewData();
            vm.MenuItem = menuItem;
            vm.NutritionalInformation = nutritionalInfo;

            return View(vm);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            MenuItemsViewData vm = new MenuItemsViewData();
            return View(vm);
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuItem, NutritionalInformation")] MenuItemsViewData data, IFormFile file)
        {
            if (!ModelState.IsValid) { return Problem(); }

            data.MenuItem.ImageUrl = await UpdateOrAddImageAsync(data, file);

            _unitOfWork.MenuItemsRepository.Add(data.MenuItem);
            _unitOfWork.SaveChanges();

            // get the primary key value, append that to nutional information foreign and then add into the table 
            data.NutritionalInformation.MenuItemId = data.MenuItem.Id;

            _unitOfWork.NutritionalInformationRepository.Add(data.NutritionalInformation);
            _unitOfWork.SaveChanges();


            TempData["CRUDMenuSuccess"] = "New item has been successfully created!";
            return RedirectToAction(nameof(Index));

        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the menu item associated with the id
            var menuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(u => u.Id == id);
            //Get the corresponding nutritional information 
            var nutritionalInfo = _unitOfWork.NutritionalInformationRepository.GetFirstOrDefault(m => m.MenuItemId == id);

            // Check if we have both value from the database
            if (menuItem == null || nutritionalInfo == null)
            {
                return NotFound();
            }

            //Construct a view model object to display/return
            MenuItemsViewData vm = new MenuItemsViewData();
            vm.MenuItem = menuItem;
            vm.NutritionalInformation = nutritionalInfo;

            return View(vm);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("MenuItem, NutritionalInformation")] MenuItemsViewData data, IFormFile? file)
        {
            // If there is no data that has been passed through, or the id fields and foreign key fields don't match up, then return not found
            if ((data == null) || (id != data.MenuItem.Id) && (id != data.NutritionalInformation.MenuItemId))
            {
                return NotFound();
            }

            // and re validate the model state
            if (ModelState.IsValid)
            {
             
                // Add images to the wwwroot and get the current url for that image
                var newImageUrl = await UpdateOrAddImageAsync(data, file);

                // If the image was updated, then we update the image url other we keep the old url
                data.MenuItem.ImageUrl = (!string.IsNullOrEmpty(newImageUrl)) ? newImageUrl : data.MenuItem.ImageUrl;

                // Save changes to database
                _unitOfWork.MenuItemsRepository.Update(data.MenuItem);
                _unitOfWork.SaveChanges();

                // get the primary key value, append that to nutional information foreign and then add into the table 
                data.NutritionalInformation.MenuItemId = data.MenuItem.Id;

                _unitOfWork.NutritionalInformationRepository.Update(data.NutritionalInformation);
                _unitOfWork.SaveChanges();

                TempData["CRUDMenuSuccess"] = "Menu Item has been successfully Edited!";
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(m => m.Id == id);
            {
                var wwwRootPath = _hostEnvironment.WebRootPath;
                // We get the old image

                var currentImagePath = menuItem.ImageUrl.TrimStart('\\');

                // We delete the old image
                if (System.IO.File.Exists(wwwRootPath + currentImagePath))
                {
                    System.IO.File.Delete(wwwRootPath + currentImagePath);
                }

                _unitOfWork.MenuItemsRepository.Delete(menuItem);
            }

            _unitOfWork.SaveChanges();
            TempData["CRUDMenuSuccess"] = "Menu Item has been successfully Delete!";
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _unitOfWork.MenuItemsRepository.FindAny(e => e.Id == id);
        }

        private async Task<string> UpdateOrAddImageAsync(MenuItemsViewData data, IFormFile file)
        {
            string basePath = "";
            string filename = "";
            string extension = "";
            // Generating the image file
            // Getting the wwwroot path
            string wwwRootPath = _hostEnvironment.WebRootPath;

            // If a file was uploaded
            if (file != null)
            {
                basePath = @"/Images/MenuItemImages/";
                // Generate a unique file name 
                filename = Guid.NewGuid().ToString();
                // The final location of where the file will be uploaded
                var uploads = Path.Combine(wwwRootPath, "Images", "MenuItemImages");
                extension = Path.GetExtension(file.FileName);

                // Ensure the directory exists, if not, create it
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                /* When updating an already existing image,
                 * we need to delete the existing image before copying 
                 * a new one to the images folder.
                 */

                // If the menuitem already has an image associated with it
                if (data.MenuItem.ImageUrl != null)
                {
                    // We get the old image
                    var oldImage =  data.MenuItem.ImageUrl.TrimStart('\\');
                    // We delete the old image
                    if (System.IO.File.Exists(wwwRootPath + oldImage))
                    {
                        System.IO.File.Delete(wwwRootPath + oldImage);
                    }
                }


                // Copy the file into the MenuItemImages folder
                using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }


            }

            return basePath + filename + extension;
        }
    }
}
