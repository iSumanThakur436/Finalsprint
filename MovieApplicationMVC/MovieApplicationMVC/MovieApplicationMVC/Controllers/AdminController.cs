using MovieApplicationMVC.Models;
using MovieApplicationMVC.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;
using System;

namespace FrontendDesignMovieApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly string baseUri = "http://localhost:49681/";

        // Common method to fetch data from the API
        private async Task<T> GetApiData<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(baseUri + endpoint); // baseUri should be the Web API base URL
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else
                {
                    // Log the error or show a message
                    return default(T);  // In case the API response is not successful
                }
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/MovieList
        public async Task<ActionResult> MovieList()
        {
            var movies = await GetApiData<List<Movie>>("GetAllMovies");
            return View(movies);
        }

        // ADD MOVIE 
        // GET: Admin/AddMovie
        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        // POST: Admin/AddMovie
        [HttpPost]
        public async Task<ActionResult> AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(baseUri + "AddMovie", movie);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("MovieList");
                }
            }
            ViewBag.Error = "Failed to add movie. Please check the details.";
            return View(movie);
        }

        // GET: Admin/EditMovie/5
        public async Task<ActionResult> EditMovie(string id)
        {
            var movie = await GetApiData<Movie>($"GetMovieById/{id}");
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/EditMovie/5
        [HttpPost]
        public async Task<ActionResult> EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(baseUri + "UpdateMovie", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Movie updated successfully!";
                        return RedirectToAction("MovieList");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error updating movie!");
                    }
                }
            }
            ViewBag.Error = "Failed to update movie. Please try again.";
            return View(movie);
        }

        // DELETE MOVIE
        public async Task<ActionResult> DeleteMovie(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(baseUri + "DeleteMovie/" + id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("MovieList");
            }
            return View("Error");
        }
        public async Task<ActionResult> GetMovieDetails(string id)
        {
            // Fetch movie details from the Web API
            var movie = await GetApiData<Movie>($"GetMovieById/{id}");

            if (movie == null)
            {
                return HttpNotFound(); // Return 404  movie not found
            }

            return View(movie);
        }

        // THEATER PART

        private readonly TheaterRepository _theaterRepository;
        private readonly UserRepository _userRepository;
        public AdminController()
        {
            _theaterRepository = new TheaterRepository();
            _userRepository = new UserRepository();

        }

        // GET: Admin/Theaters
        public ActionResult AdminTheater()
        {
            var theaters = _theaterRepository.GetAllTheaters();
            return View(theaters);
        }

        // GET: Admin/AddTheater
        [HttpGet]
        public ActionResult AddTheater()
        {
            //if (Session["UserType"]?.ToString() != "Admin")
            //    return RedirectToAction("Index");

            return View();
        }

        // POST: Admin/AddTheater
        [HttpPost]
        public ActionResult AddTheater(Theater theater)
        {
            //if (Session["UserType"]?.ToString() != "Admin")
            //    return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var isAdded = _theaterRepository.AddTheater(theater);
                if (isAdded)
                    return RedirectToAction("Theater");

                ViewBag.Error = "Failed to create theater.";
            }
            return View(theater);
        }

        // GET: Admin/EditTheater/5
        [HttpGet]
        public ActionResult UpdateTheater(string id)
        {
            //if (Session["UserType"]?.ToString() != "Admin")
            //    return RedirectToAction("Index");

            var theater = _theaterRepository.GetTheaterById(id);
            return View(theater);
        }


        [HttpPost]
        public ActionResult UpdateTheater(Theater theater)
        {
            //if (Session["UserType"]?.ToString() != "Admin")
            //    return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var isUpdated = _theaterRepository.UpdateTheater(theater);
                if (isUpdated)
                    return RedirectToAction("Theater");

                ViewBag.Error = "Failed to update theater.";
            }
            return View(theater);
        }

        // DELETE THEATER
        public ActionResult DeleteTheater(string id)
        {
            //if (Session["UserType"]?.ToString() != "Admin")
            //    return RedirectToAction("Index");


            var isDeleted = _theaterRepository.DeleteTheater(id);
            if (isDeleted)
                return RedirectToAction("Theater");

            ViewBag.Error = "Failed to delete theater.";
            return RedirectToAction("Theater");
        }





        // USER PART


        // GET: Admin/Users
        public async Task<ActionResult> Users()
        {
            var users = await _userRepository.GetAllUsers();
            return View(users);
        }



        // GET: Admin/AddUser
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        // POST: Admin/AddUser
        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(baseUri + "api/User/Register", user); // Register API endpoint
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "User added successfully!";
                        return RedirectToAction("Users");
                    }
                }
            }
            ViewBag.Error = "Failed to add user. Please check the details.";
            return View(user);
        }


        public ActionResult UserDetails(string id)
        {
            var user = _userRepository.GetUserById(id);
            return View(user);
        }




    }
}

