using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentRepository _repository;

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("ProcessPayment")]
        public IHttpActionResult ProcessPayment([FromBody] Entities.Payment payment)
        {
            try
            {
                // Simulate Razorpay Payment API integration
                RazorpayClient client = new RazorpayClient("YOUR_KEY_ID", "YOUR_SECRET");

                var options = new Dictionary<string, object>
                {
                    { "amount", payment.Amount * 100 }, // Razorpay requires amount in paise
                    { "currency", "INR" },
                    { "receipt", payment.PaymentId }
                };

                var order = client.Order.Create(options);

                // Simulate payment success
                payment.PaymentStatus = "Success"; // Simulate success response
                payment.PaymentDate = DateTime.Now;

                _repository.AddPayment(payment);

                return Ok("Payment processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Payment processing failed: " + ex.Message);
            }
        }

        [HttpGet, Route("GetByUser/{userId}")]
        public IHttpActionResult GetPaymentsByUser(string userId)
        {
            try
            {
                var payments = _repository.GetAllPaymentsByUser(userId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllGroupedByUser")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAllPaymentsGroupedByUser()
        {
            try
            {
                var groupedPayments = _repository.GetAllPaymentsGroupedByUser();
                return Ok(groupedPayments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
