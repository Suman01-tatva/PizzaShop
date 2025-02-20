using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Models;
using PizzaShop.Utils;
using PizzaShop.ViewModels;
namespace PizzaShop.Controllers;

public class UserController : Controller
{
    private readonly PizzashopContext _context;

    public UserController(PizzashopContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var AuthToken = Request.Cookies["SuperSecretAuthToken"];

        if (string.IsNullOrEmpty(AuthToken))
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            var email = Request.Cookies["email"];
            var account = _context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var role = _context.Roles.FirstOrDefault(a => a.Id == account.RoleId).Name;
            var user = _context.Users.FirstOrDefault(a => a.Email == account.Email);
            var country = _context.Countries.FirstOrDefault(a => a.Id == user.CountryId);
            var city = _context.Cities.FirstOrDefault(a => a.Id == user.CityId);
            var state = _context.States.FirstOrDefault(a => a.Id == user.StateId);

            var AllCountries = await _context.Countries.ToListAsync();
            var AllStates = _context.States.Where(s => s.CountryId == country.Id).ToList();
            var AllCities = _context.Cities.Where(c => c.StateId == city.Id).ToList();
            ViewBag.AllCountries = AllCountries;
            ViewBag.AllCities = AllCities;
            ViewBag.AllStates = AllStates;

            ViewData["role"] = role;
            ViewData["email"] = email;
            ViewData["FirstName"] = user?.FirstName;
            ViewData["LastName"] = user?.LastName;
            // Console.WriteLine("Data " + AllStates[1].Name);
            Console.WriteLine("Data " + user?.Address);
            Console.WriteLine("Data " + user?.Email);
            var userViewModel = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                // Phone = user.Phone,
                Country = country.Id.ToString(),
                State = state.Id.ToString(),
                City = city.Id.ToString(),
                ZipCode = user.Zipcode,
                Address = user.Address
            };
            return View(userViewModel);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var email = Request.Cookies["email"];

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return View();
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.UserName;
            user.Phone = model.Phone;
            user.City.Name = model.City;
            user.State.Name = model.State;
            user.Country.Name = model.Country;
            user.Address = model.Address;
            user.Zipcode = model.ZipCode;
            await _context.SaveChangesAsync();
            var AllCountries = await _context.Countries.ToListAsync();
            var AllStates = _context.States.Where(s => s.CountryId == user.CountryId).ToList();
            var AllCities = _context.Cities.Where(c => c.StateId == user.StateId).ToList();
            var role = _context.Roles.FirstOrDefault(a => a.Id == user.RoleId).Name;

            ViewBag.AllCountries = AllCountries;
            ViewBag.AllCities = AllCities;
            ViewBag.AllStates = AllStates;
            ViewData["ProfileSuccessMessage"] = "Profile Updated SucessFully";
            ViewData["role"] = role;
            ViewData["email"] = email;
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            return View();
        }
        else
        {
            return View();
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]


    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            // var data = CookieUtils.GetUserData(Request);
            // Console.WriteLine("Email" + data.Email);
            string? email = Request.Cookies["email"];
            Console.WriteLine("email", email);
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);
            var HashPassword = PasswordUtills.HashPassword(model.CurrentPassword);
            if (user?.Password == HashPassword)
            {
                string changedPassword = PasswordUtills.HashPassword(model.NewPassword);
                user.Password = changedPassword;
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("AdminDashboard", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Please Enter valid password");
                return View();
            }
        }
        return View();
    }
}