﻿using System.ComponentModel.DataAnnotations;
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
        public MenuItem MenuItem { get; set; }

        [Required]
        [Display(Name = "Calories (kcal)")]
        public int Calories { get; set; }

        [Required]
        [Display(Name = "Carbs (g)")]
        public int Carbs { get; set; }

        [Required]
        [Display(Name = "Sugar (g)")]
        public int Sugar { get; set; }

        [Required]
        [Display(Name = "Protein (g)")]
        public int Protein { get; set; }

        [Required]
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