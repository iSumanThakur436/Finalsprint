using System;
using System.Linq;
using System.Web.Http;
using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController()
        {
            _paymentRepository = new PaymentRepository(new MovieAppContext());
        }

        // Get all payments
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            return Ok(payments);
        }

        // Get payments by user ID
        [HttpGet]
        [Route("user/{userId}")]
        public IHttpActionResult GetPaymentsByUser(string userId)
        {
            var payments = _paymentRepository.GetPaymentsByUser(userId);
            return Ok(payments);
        }

        // Create a new payment entry
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreatePayment(Payment payment)
        {
            if (payment == null)
                return BadRequest("Payment cannot be null.");

            try
            {
                var createdPayment = _paymentRepository.CreatePayment(payment);
                return Ok(createdPayment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Process payment and mark status as successful
        [HttpPost]
        [Route("process")]
        public IHttpActionResult ProcessPayment(Payment payment)
        {
            if (payment == null)
                return BadRequest("Payment cannot be null.");

            try
            {
                var createdPayment = _paymentRepository.CreatePayment(payment);
                createdPayment.PaymentStatus = "Successful";
                return Ok(createdPayment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Delete a payment by PaymentId
        [HttpDelete]
        [Route("delete/{paymentId}")]
        public IHttpActionResult DeletePayment(string paymentId)
        {
            try
            {
                _paymentRepository.DeletePayment(paymentId);
                return Ok("Payment deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
