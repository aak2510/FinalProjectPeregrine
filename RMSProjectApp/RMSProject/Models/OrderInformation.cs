using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RMSProject.Models
{
    /// <summary>
    /// This is a class to bind and collect data for the Order Details that will be stored and used for payments.
    /// It includes a list of all the items purchased, the customers details, the date and time the order was made,
    /// and the total basket price.
    /// </summary>
    public class OrderInformation
    {
        [BindNever]
        public int OrderInformationId { get; set; }

        // The list of items in the basket when the order was placed.
        public List<OrderItemInBasket>? OrderDetails { get; set; }


        // Customer information fields
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [StringLength(150)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; } = string.Empty;

        [Display(Name = "Address Line 2")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter your Postcode")]
        [Display(Name = "Postcode")]
        [StringLength(10, MinimumLength = 4)]
        public string PostCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your city")]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your country")]
        [StringLength(50)]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;


        // Total basket price
        [BindNever]
        [DataType(DataType.Currency)]
        public decimal OrderTotal { get; set; }


        // The time the order was created
        [BindNever]
        public DateTime OrderPlacedTimeAndDate { get; set; }
    }
}
