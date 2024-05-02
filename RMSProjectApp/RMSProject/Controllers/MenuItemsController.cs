using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMSProject.Data;
using RMSProject.Models;
using RMSProject.ViewModels;

namespace RMSProject.Controllers
{
    public class MenuItemsController : Controller
    {
        // Dependency Injection (DI) for the database being used
        private readonly ModelsDbContext _context;

        public MenuItemsController(ModelsDbContext context)
        {
            _context = context;
        }

        // Don't need the HttpPost attribute because the method isn't changing the state of the app, just filtering data.
        // GET: MenuItems
        public async Task<IActionResult> Index(string searchString)
        {

            if (_context.MenuItem == null)
            {
                return Problem("Menu Item Entitiy set is null.");
            }

            // Get all the movies - LINQ statement (default behaviour)
            var menuItems = from m in _context.MenuItem
                            select m;

            // If there is information that is submitted in the search field
            if (!String.IsNullOrEmpty(searchString))
            {
                // return all the items which contains the item name specified in the searchString
                menuItems = menuItems.Where(s => s.ItemName!.Contains(searchString));
            }




            // The LINQ query is run against the database/executed and we return the result as a list
            return View(await menuItems.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem.FirstOrDefaultAsync(m => m.Id == id);

            var nutritionalInfo = await _context.NutritionalInformation.FirstOrDefaultAsync(m => m.MenuItemId == id);

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
        public async Task<IActionResult> Create([Bind("MenuItem, NutritionalInformation")] MenuItemsViewData data)
        {

            _context.MenuItem.Add(data.MenuItem);
            await _context.SaveChangesAsync();
            // get the primary key value, append that to nutional information foreign and then add into the table 
            data.NutritionalInformation.MenuItemId = data.MenuItem.Id;
            _context.NutritionalInformation.Add(data.NutritionalInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem.FindAsync(id);

            var nutritionalInfo = await _context.NutritionalInformation.FirstOrDefaultAsync(m => m.MenuItemId == id);


            if (menuItem == null || nutritionalInfo == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> Edit(int id, [Bind("MenuItem, NutritionalInformation")] MenuItemsViewData data)
        {
            // If there is no data that has been passed through, or the id fields and foreign key fields don't match up, then return not found
            if ((data == null) || (id != data.MenuItem.Id) && (id != data.NutritionalInformation.MenuItemId))
            {
                return NotFound();
            }

            // Otherwise, as the reference Navigational property is null when passed through, we need to reset it.
            // This is because the model is a complex model and thus the state will not be valid.
            // So we set the property to the current MneuItem we are dealing with.
            data.NutritionalInformation.MenuItem = data.MenuItem;

            // Once we have set the value, we need to clear the modestate value associated with that specific field
            ModelState.ClearValidationState("NutritionalInformation.MenuItem");

            // and re validate the model state
            if (TryValidateModel(data.NutritionalInformation.MenuItem, "NutritionalInformation.MenuItem"))
            {
                try
                {
                    // Save changes to the individual tables
                    _context.MenuItem.Update(data.MenuItem);
                    await _context.SaveChangesAsync();
                    _context.NutritionalInformation.Update(data.NutritionalInformation);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(data.MenuItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var menuItem = await _context.MenuItem
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItem.Remove(menuItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItem.Any(e => e.Id == id);
        }
    }
}
