using RMSProject.Models;

namespace RMSProject.ViewModels
{
    public class MenuItemsViewData
    {

        public MenuItem MenuItem { get; set; }


        // Might be able to shorten these two and just show Ingredients
        // As the junction table (MenuIngredients) won't be displayed
        public Ingredients Ingredients { get; set; }

        public MenuIngredients MenuIngredients { get; set; }

        public NutrionalInformation NutrionalInformation { get; set; }
    }
}
