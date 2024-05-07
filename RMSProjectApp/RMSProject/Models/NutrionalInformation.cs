using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RMSProject.Models
{
    public class NutritionalInformation
    {
        // Primary Key
        public int Id { get; set; }

        // Foreign Key to link to menu items
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        // Reference Navigation property to MenuItem
        [ValidateNever]
        public MenuItem MenuItem { get; set; }

        [Required]
        [Range(0, 3000, ErrorMessage = "Please enter a value between 0 and 3000")]
        [Display(Name = "Calories (kcal)")]
        public int Calories { get; set; }

        [Required]
        [Range(0, 3000, ErrorMessage = "Please enter a value between 0 and 3000")]
        [Display(Name = "Carbs (g)")]
        public int Carbs { get; set; }

        [Required]
        [Range(0, 3000, ErrorMessage = "Please enter a value between 0 and 3000")]
        [Display(Name = "Sugar (g)")]
        public int Sugar { get; set; }

        [Required]
        [Range(0, 3000, ErrorMessage = "Please enter a value between 0 and 3000")]
        [Display(Name = "Protein (g)")]
        public int Protein { get; set; }

        [Required]
        [Range(0, 3000, ErrorMessage = "Please enter a value between 0 and 3000")]
        [Display(Name = "Fats (g)")]
        public int Fats { get; set; }


        [Display(Name = "Vegan")]
        public bool IsVegan { get; set; }


        [Display(Name = "Vegetarian")]
        public bool IsVegetarian { get; set; }


        [Display(Name = "Nuts")]
        public bool HasNuts { get; set; }
    }
}
