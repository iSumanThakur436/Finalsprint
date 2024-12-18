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
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:49681/api/booking/") };
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
                BookingId = "B" + new Random().Next(1000, 9999),
                UserId = Session["UserId"].ToString(),
                MovieId = movieTheaterDetails.MovieId,
                TheaterId = movieTheaterDetails.TheaterId,
                ShowTimeId = movieTheaterDetails.ShowTimeId,
                BookingDate = bookingDate,
                NumberOfSeats = numberOfSeats,
                TotalPrice = numberOfSeats * movieTheaterDetails.TicketPrice,
                Status = "Pending"
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("Create", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var bookingResponse = JsonConvert.DeserializeObject<Booking>(response.Content.ReadAsStringAsync().Result);
                    return RedirectToAction("Payment", "Payment", new { bookingId = bookingResponse.BookingId, amount = bookingResponse.TotalPrice });
                }

                throw new Exception("Error creating booking.");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Booking", "Create"));
            }
        }

        public ActionResult Tickets(string bookingId)
        {
            var ticket = new Ticket
            {
                TicketId = "TK123",
                BookingId = bookingId,
                ShowTimeId = "ST456",
                SeatNumber = "1,2"
            };

            return View(ticket);
        }
    }
}
