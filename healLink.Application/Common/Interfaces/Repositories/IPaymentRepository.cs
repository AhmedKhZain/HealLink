using HealLink.Domain.Payments;

namespace healLink.Application.Common.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task AddPaymentAsync(Payment paymentToAdd);
    Task<Payment?> GetpaymentById(Guid id);
    Task<Payment?> GetpaymentByRequestId(Guid requestId);
    void UpdatePayment(Payment payment);
}



