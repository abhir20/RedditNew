using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RedditNew.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace RedditNew.Application.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetToken()
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "your-username"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Token expiration time
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (IsValidUser(user.Username, user.Password))
            {
                // Authentication successful
                // You can store user information in a session or set authentication cookies.
                // Redirect to a secure page or dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(user);
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // You should implement your authentication logic here.
            // Check if the provided username and password are valid.
            // You might compare them against a database or other data source.
            // Return true if the user is valid, otherwise, return false.
            return (username == "example" && password == "password");
        }
    }
}
