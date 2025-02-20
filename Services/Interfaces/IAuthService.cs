using PizzaShop.Models;


namespace PizzaShop.Services.Interfaces;

public interface IAuthService
{
    Task<Account?> AuthenticateUser(string email, string password);   //Used Account instead of User
    Task<Role?> CheckRole(string role);
}
