using System.ComponentModel.DataAnnotations;
namespace PizzaShop.Models;
public class LoginForm
{
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}