using System;
using System.Collections.Generic;
using System.Linq;
using MovieApplicationSprint.Entities;

namespace MovieApplicationSprint.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MovieAppContext _context;

        public PaymentRepository(MovieAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public IEnumerable<Payment> GetPaymentsByUser(string userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).ToList();
        }

        public Payment CreatePayment(Payment payment)
        {
            payment.PaymentId = Guid.NewGuid().ToString();
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public void DeletePayment(string paymentId)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }

        public Payment GetPaymentById(string paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }
    }
}
