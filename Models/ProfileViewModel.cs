using System.ComponentModel.DataAnnotations;

namespace PizzaShop.ViewModels;

public class ProfileViewModel
{
    [Required(ErrorMessage = "FirstName is required.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "State is required")]
    public string State { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "ZipCode is required")]
    public string ZipCode { get; set; } = null!;
}