using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;

namespace PizzaShop.Controllers;

public class HomeController : Controller
{
    private readonly PizzashopContext _context;

    // private readonly IEmailSender _emailSender;
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
                TempData["Email"] = email;
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
            var user = _context.Accounts.FirstOrDefaultAsync(u => u.Email == model.Email);

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
            // var user = await _context.Accounts.FindByEmailAsync(model.Email);
        }
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