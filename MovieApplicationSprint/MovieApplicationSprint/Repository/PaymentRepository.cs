using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MovieAppContext _context;

        public PaymentRepository()
        {
            _context = new MovieAppContext();
        }

        public void AddPayment(Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();

                // Update booking status if payment successful
                if (payment.PaymentStatus == "Success")
                {
                    UpdateBookingStatus(payment.BookingId, "Confirmed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing payment: " + ex.Message);
            }
        }

        public List<Payment> GetAllPaymentsByUser(string userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).ToList();
        }

        public List<IGrouping<string, Payment>> GetAllPaymentsGroupedByUser()
        {
            return _context.Payments.GroupBy(p => p.UserId).ToList();
        }

        public Payment GetPaymentByBooking(string bookingId)
        {
            return _context.Payments.FirstOrDefault(p => p.BookingId == bookingId);
        }

        public void UpdateBookingStatus(string bookingId, string status)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                _context.SaveChanges();
            }
        }
    }
}
