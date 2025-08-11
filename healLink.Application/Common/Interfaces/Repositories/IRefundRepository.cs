using HealLink.Domain.Payments;

namespace healLink.Application.Common.Interfaces.Repositories;

public interface IRefundRepository
{
    Task AddRefundAsync(RefundItem refund);
    void DeleteRefundAsync(RefundItem refund);

}