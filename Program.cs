using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;
using PizzaShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("pizzashopDbConn");
builder.Services.AddDbContext<PizzashopContext>(q => q.UseNpgsql(conn));
builder.Services.AddControllersWithViews();

// builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddDataProtection().SetApplicationName("PizzaShop");
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}");

app.Run();