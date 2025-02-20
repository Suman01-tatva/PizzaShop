using System.Text.Json.Serialization;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;
using PizzaShop.Services;
using PizzaShop.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("pizzashopDbConn");
builder.Services.AddDbContext<PizzashopContext>(q => q.UseNpgsql(conn));
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddDataProtection().SetApplicationName("PizzaShop");
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddControllersWithViews();
// builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Add authentication using cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index"; // Redirect to login if not authenticated
        options.LogoutPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect if unauthorized
    });

builder.Services.AddAuthorization();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();