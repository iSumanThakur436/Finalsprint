using MovieApplicationSprint.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        List<Payment> GetAllPaymentsByUser(string userId);
        List<IGrouping<string, Payment>> GetAllPaymentsGroupedByUser();
        Payment GetPaymentByBooking(string bookingId);
        void UpdateBookingStatus(string bookingId, string status);
    }
}
