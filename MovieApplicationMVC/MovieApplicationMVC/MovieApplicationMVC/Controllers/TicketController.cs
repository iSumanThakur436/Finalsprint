using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web.Mvc;
using MovieApplicationMVC.Models;
using Newtonsoft.Json;

namespace MovieApplicationMVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly HttpClient _httpClient;

        public TicketController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:49681/api/tickets/") };
        }

        // Create ticket after payment
        public ActionResult CreateAfterPayment(string bookingId, string showTimeId, string seatNumber)
        {
            try
            {
                var ticket = new Ticket
                {
                    BookingId = bookingId,
                    ShowTimeId = showTimeId,
                    SeatNumber = seatNumber
                };

                var response = _httpClient.PostAsJsonAsync("CreateAfterPayment", ticket).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error creating ticket.");

                return RedirectToAction("Tickets", new { bookingId });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Ticket", "CreateAfterPayment"));
            }
        }

        // Display tickets by booking
        public ActionResult Tickets(string bookingId)
        {
            try
            {
                var response = _httpClient.GetAsync($"GetByBooking/{bookingId}").Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error fetching tickets.");

                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(response.Content.ReadAsStringAsync().Result);

                return View(tickets);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Ticket", "Tickets"));
            }
        }

        // Download ticket as PDF
        public FileResult DownloadTicket(string ticketId)
        {
            try
            {
                var response = _httpClient.GetAsync($"GetById/{ticketId}").Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error fetching ticket details.");

                var ticket = JsonConvert.DeserializeObject<Ticket>(response.Content.ReadAsStringAsync().Result);

                var ticketDetails = $"Ticket ID: {ticket.TicketId}\nBooking ID: {ticket.BookingId}\nShow Time: {ticket.ShowTimeId}\nSeat Number: {ticket.SeatNumber}";

                var byteArray = System.Text.Encoding.UTF8.GetBytes(ticketDetails);
                return File(byteArray, "application/pdf", $"Ticket_{ticket.TicketId}.pdf");
            }
            catch (Exception ex)
            {
                throw new Exception("Error downloading ticket.");
            }
        }
    }
}
