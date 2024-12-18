using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using MovieApplicationMVC.Models;
using Newtonsoft.Json;

namespace MovieApplicationMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:49681/api/payment/") };
        }

        // Payment Page
        [HttpGet]
        public ActionResult Payment(string bookingId, int amount)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "User");

            var paymentDetails = new PaymentDetails
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = "Credit Card",
                PaymentStatus = "Pending",
                PaymentDate = DateTime.Now,
                UserId = Session["UserId"].ToString()
            };

            return View(paymentDetails);
        }

        // Process Payment
        [HttpPost]
        public ActionResult ProcessPayment(PaymentDetails paymentDetails)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(paymentDetails), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("process", content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error processing payment.");
               
                return RedirectToAction("CreateAfterPayment", "Ticket", new { bookingId = paymentDetails.BookingId,showTimeId="ST01", seatNumber=3 });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Payment", "ProcessPayment"));
            }
        }
    }
}
