using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models;

public class ResetPasswordModel
{

    [Required(ErrorMessage = "New Password is Required")]
    public string NewPassword { set; get; } = null!;

    [Required(ErrorMessage = "Confirm New Password is Required")]
    public string ConfirmNewPassword { set; get; } = null!;
}
