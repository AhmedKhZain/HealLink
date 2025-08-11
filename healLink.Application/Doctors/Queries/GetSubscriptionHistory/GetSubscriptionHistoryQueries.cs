using ErrorOr;
using MediatR;
using Stripe;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HealLink.Contracts.Doctors;

public record GetSubscriptionHistoryQuery
    (Guid DoctorId)
    :IRequest<ErrorOr<IEnumerable<SubscriptionHistoryResponse>>>;
