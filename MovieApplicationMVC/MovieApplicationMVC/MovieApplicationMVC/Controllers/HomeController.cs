using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using MovieApplicationMVC.Models;
using System;

namespace MovieApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiBaseUrl = "http://localhost:49681";

        public async Task<ActionResult> Index()
        {
            List<Movie> movies = new List<Movie>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiBaseUrl);

                HttpResponseMessage response = await client.GetAsync("/GetAllMovies");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                }
                else
                {
                    ViewBag.Error = "Failed to load movies.";
                }
            }

            return View(movies);
        }
        public async Task<ActionResult> SearchMovies(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ViewBag.Error = "Keyword cannot be empty.";
                return RedirectToAction("Index");
            }

            List<Movie> movies = new List<Movie>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseUrl);

                    HttpResponseMessage response = await client.GetAsync($"/SearchMovies/{keyword}");
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // Pass search results to the new SearchResults view
            return View("SearchResults", movies);
        }



        public async Task<ActionResult> MovieDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound("Movie ID is required.");
            }

            Movie movie = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage response = await client.GetAsync($"/GetMovieById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    movie = JsonConvert.DeserializeObject<Movie>(json);
                }
                else
                {
                    return HttpNotFound("Movie not found.");
                }
            }

            return View(movie);
        }

        // Step 1: Select Location
        public ActionResult SelectLocation(string movieId)
        {
            ViewBag.MovieId = movieId;
            return View();
        }

        // Step 2: Fetch Theaters for the Selected Location and Movie
        public async Task<ActionResult> TheaterList(string movieId, string city)
        {
            if (string.IsNullOrEmpty(movieId) || string.IsNullOrEmpty(city))
            {
                return HttpNotFound("Movie ID and City are required.");
            }

            List<MovieTheaterDetails> theaterList = new List<MovieTheaterDetails>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage response = await client.GetAsync($"/GetTheaterByMovieId/{movieId}/{city}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    theaterList = JsonConvert.DeserializeObject<List<MovieTheaterDetails>>(json);
                }
                else
                {
                    ViewBag.Error = "Failed to load theater details.";
                }
            }

            ViewBag.City = city;
            return View(theaterList);
        }
    }
}

    

