using Azure.Core;
using PizzaShop.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };
            string userData = JsonSerializer.Serialize(user, options);

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

        public static Account? GetUserData(HttpRequest request)
        {
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };

            // Store user details in a cookie for 3 days
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(3),
                HttpOnly = true,
                Secure = true,
                IsEssential = true
            };
            var data = request.Cookies["userData"];
            return string.IsNullOrEmpty(data) ? null : JsonSerializer.Deserialize<Account>(data, options);
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