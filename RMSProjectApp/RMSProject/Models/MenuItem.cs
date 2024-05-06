using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RMSProject.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Menu Item name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Item description")]
        [MaxLength(255)]
        public string ItemDescription { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price of Menu Item")]
        public decimal ItemPrice { get; set; }

        [Required]
        [Display(Name = "Type of meal")]
        public MealType TypeOfMeal { get; set; }

        public string ImageUrl { get; set; }
    }

  
   
}
