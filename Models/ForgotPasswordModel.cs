using System.ComponentModel.DataAnnotations;
namespace PizzaShop.Models;
public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = null!;
}