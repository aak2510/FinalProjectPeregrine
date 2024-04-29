using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMSProject.Models
{
   
    public class MenuIngredients
    {
        // Junction table has a primary which is the combination of the foreign keys
        // Key attribute applied to both properties to specificy composite keys
        [Key, Column(Order = 0)]
        public int IngredientsId { get; set; }
        // Reference Navigation property to Ingredients
        public Ingredients Ingredients { get; set; }



        [Key, Column(Order = 1)]
        public int MenuItemId { get; set; }
        // Reference Navigation property to MenuItem
        public MenuItem MenuItem{ get; set; }
    }
}
