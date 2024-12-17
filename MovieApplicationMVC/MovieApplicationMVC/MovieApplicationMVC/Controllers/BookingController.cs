using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using MovieApplicationMVC.Models;
using Newtonsoft.Json;

namespace MovieApplicationMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:49681/Booking/") };
        }

        // Render Create Booking form with prefilled MovieTheaterDetails
        [HttpGet]
        public ActionResult Create(string movieId, string theaterId, string showTimeId, string title, string theaterName, int ticketPrice, string showDateTime)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "User");

            // Parse showDateTime to DateTime format
            DateTime parsedShowDateTime = DateTime.Parse(showDateTime);

            var movieTheaterDetails = new MovieTheaterDetails
            {
                UserId = Session["UserId"].ToString(),
                MovieId = movieId,
                TheaterId = theaterId,
                ShowTimeId = showTimeId,
                Title = title,
                TheaterName = theaterName,
                ShowDateTime = parsedShowDateTime,
                TicketPrice = ticketPrice
            };

            return View(movieTheaterDetails);
        }

        [HttpPost]
        public ActionResult Create(MovieTheaterDetails movieTheaterDetails, int numberOfSeats, DateTime bookingDate)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "User");

            if (numberOfSeats <= 0)
            {
                ModelState.AddModelError("", "Number of seats must be at least 1.");
                return View(movieTheaterDetails);
            }

            var booking = new Booking
            {
                BookingId = "B" + new Random().Next(1000, 9999), // Generate BookingId
                UserId = movieTheaterDetails.UserId,
                MovieId = movieTheaterDetails.MovieId,
                TheaterId = movieTheaterDetails.TheaterId,
                ShowTimeId = movieTheaterDetails.ShowTimeId,
                BookingDate = bookingDate, // User input
                NumberOfSeats = numberOfSeats,
                TotalPrice = numberOfSeats * movieTheaterDetails.TicketPrice,
                Status = "Pending"
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("Create", content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error creating booking.");

                return RedirectToAction("GetAllBookings");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Booking", "Create"));
            }
        }

    }
}
