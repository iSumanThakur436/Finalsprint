using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MovieApplicationMVC.Models;
namespace MovieApplicationMVC.Controllers
{
    [RoutePrefix("Landing")]

    public class LandingController : Controller
    {
        // GET: Landing
        private HttpClient client;

        public LandingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:49681/");
        }

        [HttpGet]
        [Route("GetAllMovies")]

        public ActionResult GetAllMovies()

        {

            try
            {

                HttpResponseMessage response = client.GetAsync("GetAllMovies").Result;


                if (response.IsSuccessStatusCode)

                {

                    List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(

                        response.Content.ReadAsStringAsync().Result

                    );

                    return View(movies);

                }


                return View(new List<Movie>());
            }

            catch (Exception ex)

            {

                ViewBag.Error = $"An error occurred: {ex.Message}";

                return View(new List<Movie>());

            }

        }


        [HttpGet]

        [Route("GetMovieById/{id}")]

        public ActionResult GetMovieById(string id)

        {

            try
            {

                HttpResponseMessage response = client.GetAsync($"GetMovieById/{id}").Result;


                if (response.IsSuccessStatusCode)

                {

                    Movie movie = JsonConvert.DeserializeObject<Movie>(

                        response.Content.ReadAsStringAsync().Result

                    );

                    return View(movie);

                }


                return HttpNotFound("Movie not found.");

            }

            catch (Exception ex)

            {

                ViewBag.Error = $"An error occurred: {ex.Message}";

                return HttpNotFound("An error occurred while fetching the movie.");

            }

        }



        [HttpGet]

        [Route("GetMovieById/{id}")]

        public ActionResult GetMovieByName(string name)

        {

            try
            {

                HttpResponseMessage response = client.GetAsync($"GetMovieByName/{name}").Result;


                if (response.IsSuccessStatusCode)

                {

                    Movie movie = JsonConvert.DeserializeObject<Movie>(

                        response.Content.ReadAsStringAsync().Result

                    );

                    return View(movie);

                }


                return HttpNotFound("Movie not found.");

            }

            catch (Exception ex)

            {

                ViewBag.Error = $"An error occurred: {ex.Message}";

                return HttpNotFound("An error occurred while fetching the movie.");

            }

        }



        //public ActionResult SearchMovie(string query)        //{        //    if (string.IsNullOrWhiteSpace(query))        //    {        //        TempData["Error"] = "Please enter a movie name.";        //        return RedirectToAction("Index");        //    } 

        //    string apiUrl = $"http://localhost:5000//movies/search?query={query}";        //    using (var client = new HttpClient())        //    {        //        var response = client.GetAsync(apiUrl).Result;        //        if (response.IsSuccessStatusCode)        //        {        //            var movies = response.Content.ReadAsAsync<List<Movie>>().Result;        //            if (movies.Count == 1)        //            {        //                // Redirect to the movie details page if a single match is found        //                return RedirectToAction("Details", new { id = movies[0].MovieId });        //            } 

        //            // Pass multiple results to the view if necessary        //            ViewBag.Movies = movies;        //            return View("SearchResults");        //        }        //        else        //        {        //            TempData["Error"] = "Movie not found.";        //            return RedirectToAction("Index");        //        }        //    }        //} 

    


    }
}