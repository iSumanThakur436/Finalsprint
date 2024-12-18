using System;
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

        // Display details for a single ticket
        public ActionResult TicketDetails(string ticketId)
        {
            try
            {
                var response = _httpClient.GetAsync($"GetById/{ticketId}").Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error fetching ticket details.");

                var ticket = JsonConvert.DeserializeObject<Ticket>(response.Content.ReadAsStringAsync().Result);

                return View(ticket); // Pass a single ticket to the view
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Ticket", "TicketDetails"));
            }
        }
    }
}
