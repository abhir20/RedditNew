using Microsoft.AspNetCore.Mvc;
using RedditNew.Application.Models;

namespace RedditNew.Application.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (IsValidUser(user.Username, user.Password))
            {
                // Authentication successful
                // You can store user information in a session or set authentication cookies.
                // Redirect to a secure page or dashboard.
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
