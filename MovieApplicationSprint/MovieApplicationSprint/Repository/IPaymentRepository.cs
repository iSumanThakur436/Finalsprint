using System.Collections.Generic;
using MovieApplicationSprint.Entities;

namespace MovieApplicationSprint.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments();
        IEnumerable<Payment> GetPaymentsByUser(string userId);
        Payment CreatePayment(Payment payment);
        void DeletePayment(string paymentId);
        Payment GetPaymentById(string paymentId);
    }
}
