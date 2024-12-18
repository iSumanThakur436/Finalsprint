using MovieApplicationMVC.Models;
using MovieApplicationMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using System.Web.Mvc;

namespace MovieApplicationMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository;

        public UserController()
        {
            _repository = new UserRepository();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {

                user.UserId = "U" + new Random().Next(1000, 9999);
                // Exclude ConfirmPassword before sending to the API
                user.ConfirmPassword = null;

                // Default role for registration
                user.UserType = "User";

                // Send data to API
                var isRegistered = _repository.Register(user);

                if (isRegistered)
                    return RedirectToAction("Login");

                ViewBag.Error = "Registration failed. Please try again.";
            }
            return View(user);
        
    
}


[HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Login(string email, string password)
        {
            var user = _repository.Login(email, password);
            if (user != null)
            {
                // Store the session variables
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                Session["UserType"] = user.UserType;

                // Redirect based on UserType
                if (user.UserType == "Admin")
                {
                    return RedirectToAction("Index", "Admin"); // Redirect to Admin's home page
                }
                else if (user.UserType == "User")
                {
                    return RedirectToAction("Index", "Home"); // Redirect to User's home page
                }
            }

            ViewBag.Error = "Invalid credentials. Please try again.";
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Profile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            var userId = Session["UserId"].ToString();
            var user = _repository.GetUserById(userId);
            return View(user);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            var userId = Session["UserId"].ToString();
            var user = _repository.GetUserById(userId);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _repository.UpdateProfile(user);
                if (isUpdated)
                    return RedirectToAction("Profile");

                ViewBag.Error = "Failed to update profile.";
            }
            return View(user);
        }


        public ActionResult DeleteAccount()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            var userId = Session["UserId"].ToString();
            var isDeleted = _repository.DeleteUser(userId);

            if (isDeleted)
            {
                Session.Clear();
                return RedirectToAction("Register");
            }

            ViewBag.Error = "Failed to delete account.";
            return RedirectToAction("Profile");
        }

        public async Task<ActionResult> YourBookings()
        {
            // Get the current logged-in user's ID from session
            string userId = Session["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.Error = "User not logged in.";
                return RedirectToAction("Login"); // Redirect to login if no user ID is found in session
            }

            List<BookingViewModel> bookings = new List<BookingViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49681/api/"); // Replace with your actual API URL
                var response = await client.GetAsync($"Booking/GetByUser/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    bookings = await response.Content.ReadAsAsync<List<BookingViewModel>>();
                }
                else
                {
                    ViewBag.Error = "Unable to load bookings.";
                }
            }

            return View(bookings);
        }

    }
}
