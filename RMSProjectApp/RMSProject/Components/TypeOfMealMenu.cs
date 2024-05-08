using Microsoft.AspNetCore.Mvc;
using RMSProject.Models;
using RMSProject.Repositories.IRepository;
using RMSProject.ViewModels;

namespace RMSProject.Components
{
    public class TypeOfMealMenu : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeOfMealMenu(IUnitOfWork unitOfWorkd)
        {
            _unitOfWork = unitOfWorkd;
        }

        public IViewComponentResult Invoke()
        {
            var typesOfMeals = _unitOfWork.MenuItemsRepository.GetAll().OrderBy(mealType => mealType.TypeOfMeal);

            return View(typesOfMeals);
        }
    }
}
