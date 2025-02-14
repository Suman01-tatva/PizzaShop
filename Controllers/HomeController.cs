using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;

namespace PizzaShop.Controllers;

public class HomeController : Controller
{
    private readonly PizzashopContext _context;
    public HomeController(PizzashopContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginForm loginForm)
    {
        if (ModelState.IsValid)
        {
            var email = loginForm.Email;
            var password = loginForm.Password;

            var user = await _context.Accounts.FirstOrDefaultAsync(q => q.Email == email);
            if (user != null)
            {
                if (user.Password == password)
                {
                    if (loginForm.RememberMe)
                    {
                        Response.Cookies.Append("email", email);
                        Response.Cookies.Append("password", password);
                    }
                    return RedirectToAction("HomePage");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter valid credentials");
                return View("Index");
            }
        }
        return View("Index");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    public IActionResult HomePage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}