using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Current Password is Required")]
    public string CurrentPassword { set; get; } = null!;

    [Required(ErrorMessage = "New Password is Required")]
    public string NewPassword { set; get; } = null!;

    [Required(ErrorMessage = "Confirm New Password is Required")]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { set; get; } = null!;
}