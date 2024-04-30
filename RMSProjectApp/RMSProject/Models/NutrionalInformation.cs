using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RMSProject.Models
{
    public class NutrionalInformation
    {
        // Primary Key
        public int Id { get; set; }

        // Foreign Key to link to menu items
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        // Reference Navigation property to MenuItem
        public MenuItem MenuItem { get; set; }

        [Required]
        [Display(Name = "Calories")]
        public int Calories { get; set; }

        [Required]
        [Display(Name = "Carbs")]
        public int Carbs{ get; set; }

        [Required]
        [Display(Name = "Sugar")]
        public int Sugar { get; set; }

        [Required]
        [Display(Name = "Protein")]
        public int Protein { get; set; }

        [Required]
        [Display(Name = "Fats")]
        public int Fats { get; set; }

        [Required]
        [Display(Name = "Vegan")]
        public bool IsVegan { get; set; }

        [Required]
        [Display(Name = "Vegetarian")]
        public bool IsVegetarian { get; set; }

        [Required]
        [Display(Name = "Nuts")]
        public bool HasNuts { get; set; }
    }
}
