using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
            var userList = _context.Users.ToList();
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
                Phone = user.Phone,
                Country = country.Id.ToString(),
                State = state.Id.ToString(),
                City = city.Id.ToString(),
                ZipCode = user.Zipcode,
                Address = user.Address
            };
            return View(userViewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetStates(int countryId)
    {
        var states = await _context.States
            .Where(s => s.CountryId == countryId)
            .ToListAsync();
        return Json(states);
    }

    [HttpGet]
    public async Task<IActionResult> GetCities(int stateId)
    {
        var cities = await _context.Cities
            .Where(c => c.StateId == stateId)
            .ToListAsync();
        return Json(cities);
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
            user.CityId = int.Parse(model.City);
            user.StateId = int.Parse(model.State);
            user.CountryId = int.Parse(model.Country);
            user.Address = model.Address;
            user.Zipcode = model.ZipCode;
            await _context.SaveChangesAsync();
            var AllCountries = await _context.Countries.ToListAsync();
            var AllStates = _context.States.Where(s => s.CountryId == user.CountryId).ToList();
            var AllCities = _context.Cities.Where(c => c.StateId == user.StateId).ToList();
            var role = _context.Roles.FirstOrDefault(a => a.Id == user.RoleId).Name;

            ViewBag.AllCountries = new SelectList(await _context.Countries.ToListAsync(), "Id", "Name", user.CountryId);
            ViewBag.AllCities = new SelectList(await _context.States.Where(s => s.CountryId == user.CountryId).ToListAsync(), "Id", "Name", user.StateId);
            ViewBag.AllStates = new SelectList(await _context.Cities.Where(ct => ct.StateId == user.StateId).ToListAsync(), "Id", "Name", user.CityId);

            // ViewBag.AllCountries = AllCountries;
            // ViewBag.AllCities = AllCities;
            // ViewBag.AllStates = AllStates;
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

    [HttpGet]
    public IActionResult UserList(string searchString, int pageIndex = 1, int pageSize = 5, string sortOrder = "")
    {
        var userQuery = _context.Users.Where(u => u.IsDeleted == false);

        ViewData["UsernameSortParam"] = sortOrder == "username_asc" ? "username_desc" : "username_asc";
        ViewData["RoleSortParam"] = sortOrder == "role_asc" ? "role_desc" : "role_asc";

        switch (sortOrder)
        {
            case "username_asc":
                userQuery = userQuery.OrderBy(u => u.FirstName);
                break;

            case "username_desc":
                userQuery = userQuery.OrderByDescending(u => u.FirstName);
                break;

            case "role_asc":
                userQuery = userQuery.OrderBy(u => u.Role.Name);
                break;

            case "role_desc":
                userQuery = userQuery.OrderByDescending(u => u.Role.Name);
                break;

            default:
                userQuery = userQuery.OrderBy(u => u.Id);
                break;

        }

        if (!string.IsNullOrEmpty(searchString))
        {
            userQuery = userQuery.Where(u => u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.Contains(searchString));
        }

        var userList = userQuery
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        var count = _context.Users.Count();
        ViewBag.count = count;
        ViewBag.pageIndex = pageIndex;
        ViewBag.pageSize = pageSize;
        ViewBag.totalPage = (int)Math.Ceiling(count / (double)pageSize);
        ViewBag.searchString = searchString;

        if (userList == null)
        {
            ViewBag["ErrorMessage"] = "UserList is Empty";
            return View();
        }

        ViewBag.UserList = userList;
        return View();
    }
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

    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                ViewBag.UserExistError = "User Already Exist";
                return View();
            }

            user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                Phone = model.Phone,
                RoleId = model.RoleId,
                ProfileImage = model.ProfileImage,
                Zipcode = model.Zipcode,
                Address = model.Address,
                CountryId = model.CountryId,
                StateId = model.StateId,
                CityId = model.CityId,
                CreatedBy = user.Id,
            };

            _context.Users.Add(user);
            var rowsAffected = await _context.SaveChangesAsync();
            if (rowsAffected > 0)
            {
                Console.WriteLine("User created successfully.");
                TempData["SuccessMessage"] = "Profile created successfully.";
                return RedirectToAction(nameof(UserList));
            }
            else
            {
                Console.WriteLine("No changes detected.");
                TempData["ErrorMessage"] = "No changes were made.";
            }
            return View();
        }
        else
        {
            return View();
        }
    }

}