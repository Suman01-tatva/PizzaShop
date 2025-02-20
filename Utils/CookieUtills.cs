using PizzaShop.Models;
using System.Text.Json;

namespace PizzaShop.Utils
{
    public static class CookieUtils
    {
        /// <summary>
        /// Save JWT Token to Cookies
        /// </summary>
        /// <param name="response"></param>
        /// <param name="token"></param>
        public static void SaveJWTToken(HttpResponse response, string token)
        {
            response.Cookies.Append("SuperSecretAuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });
        }

        public static string? GetJWTToken(HttpRequest request)
        {
            _ = request.Cookies.TryGetValue("SuperSecretAuthToken", out string? token);
            return token;
        }

        /// <summary>
        /// Save User data to Cookies
        /// </summary>
        /// <param name="response"></param>
        /// <param name="user"></param>
        public static void SaveUserData(HttpResponse response, Account user)
        {
            string userData = JsonSerializer.Serialize(user);

            // Store user details in a cookie for 3 days
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(3),
                HttpOnly = true,
                Secure = true,
                IsEssential = true
            };
            response.Cookies.Append("UserData", userData, cookieOptions);
        }

        public static void ClearCookies(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("SuperSecretAuthToken");
            httpContext.Response.Cookies.Delete("UserData");
        }

        internal static void SaveJWTToken(HttpResponse response, object token)
        {
            throw new NotImplementedException();
        }

    }
}