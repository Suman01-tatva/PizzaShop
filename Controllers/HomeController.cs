using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;
using PizzaShop.Services;
using PizzaShop.Services.Interfaces;
using PizzaShop.Utils;

namespace PizzaShop.Controllers;

public class HomeController : Controller
{
    private readonly PizzashopContext _context;

    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    // private readonly IEmailSender _emailSender;

    public HomeController(PizzashopContext context, IAuthService authService, IJwtService jwtService)
    {
        _context = context;
        _authService = authService;
        _jwtService = jwtService;
        // _emailSender = emailSender;
    }

    public IActionResult Index()
    {
        var token = Request.Cookies["SuperSecretAuthToken"];
        var ValidateToken = _jwtService.ValidateToken(token!);
        if (ValidateToken != null)
        {
            return RedirectToAction("AdminDashBoard", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginForm model)
    {
        if (ModelState.IsValid)
        {

            //Authenticate User
            var user = await _authService.AuthenticateUser(model.Email, model.Password);
            if (user == null)
            {
                // ViewBag.ErrorMessage = "Invalid email or password.";
                // return View();
                ModelState.AddModelError("", "Please enter valid credentials");
                return View("Index");
            }

            // Generate JWT Token
            var token = _jwtService.GenerateJwtToken(user.Email, user.Role.Name);  // Add user.Id

            // Store token in cookie
            CookieUtils.SaveJWTToken(Response, token);
            Response.Cookies.Append("email", user.Email);
            // Save User Data to Cookie for Remember Me functionality.
            if (model.RememberMe)
            {
                CookieUtils.SaveUserData(Response, user);
                // Response.Cookies.Append(token, token);
            }
            TempData["Email"] = user.Email;
            // if (user.RoleId == 1)
            // {
            //     return RedirectToLocal("AdminDashboard");
            // }
            // else
            // {
            //     return RedirectToLocal("UserDashboard");
            // }
            return RedirectToAction("AdminDashboard");
        }
        return View("Index");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl); // Return the user to their original destination
        }
        return RedirectToAction("Index"); // Fallback to the default route
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // [HttpPost]
    // public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var user = _context.Accounts.FirstOrDefaultAsync(u => u.Email == model.Email);

    //         if (user != null)
    //         {
    //             string? resetLink = Url.Action("ResetPassword", "Home", new { email = model.Email, timeStamp = DateTime.UtcNow.Ticks }, Request.Scheme);

    //             await _emailSender.SendEmailAsync(model.Email, "Reset Your Password", $"Click <a href = '{resetLink}' > Here </a> to reset your password");
    //             return View("ForgotPasswordConfirmation");
    //         }
    //         return View("ForgotPasswordConfirmation");
    //     }
    //     return View(model);
    // }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                SendMail(model.Email);
            }
            return View("ForgotPasswordConfirmation");
        }
        return View();
    }

    public void SendMail(string ToEmail)
    {
        TempData["Email"] = ToEmail;
        string SenderMail = "test.dotnet@etatvasoft.com";
        string SenderPassword = "P}N^{z-]7Ilp";
        string Host = "mail.etatvasoft.com";
        int Port = 587;
        var smtpClient = new SmtpClient(Host)
        {
            Port = Port,
            Credentials = new NetworkCredential(SenderMail, SenderPassword),
            // EnableSsl = true,
        };
        string? resetLink = Url.Action("ResetPassword", "Home", new { email = ToEmail, timeStamp = DateTime.UtcNow.Ticks }, Request.Scheme);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(SenderMail),
            Subject = "To Reset Your Password",
            Body = $"Click <a href = '{resetLink}' > Here </a> to reset your password",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(ToEmail);

        smtpClient.Send(mailMessage);
    }

    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            string? email = TempData["Email"]?.ToString();
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                if (model.NewPassword != null && model.ConfirmNewPassword != null)
                {
                    if (model.NewPassword == model.ConfirmNewPassword)
                    {

                        user.Password = PasswordUtills.HashPassword(model.NewPassword);
                        _context.Update(user);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("HomePage", "Home");
                }
            }
            else
            {
                return View("ForgotPassword");
            }
        }
        return View(model);
    }

    // [Authorize(Roles = "1")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    // [Authorize]
    public IActionResult UserDashboard()
    {
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("SuperSecretAuthToken");
        Response.Cookies.Delete("UserData");
        Response.Cookies.Delete("email");
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}