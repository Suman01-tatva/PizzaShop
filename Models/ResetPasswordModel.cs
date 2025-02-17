using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models;

public class ResetPasswordModel
{

    [Required(ErrorMessage = "Email is Required")]
    public string Email { set; get; }

    [Required(ErrorMessage = "New Password is Required")]
    public string NewPassword { set; get; }

    [Required(ErrorMessage = "Confirm New Password is Required")]
    public string ConfirmNewPassword { set; get; }
}
