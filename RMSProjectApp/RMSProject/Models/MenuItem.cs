using System.ComponentModel.DataAnnotations;

namespace RMSProject.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Display(Name ="Menu Item name")]
        public string ItemName { get; set; }

        [Display(Name = "Price of Menu Item")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Type of meal")]
        public MealType TypeOfMeal { get; set; }

    }

  
   
}
