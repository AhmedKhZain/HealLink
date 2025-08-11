using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using MediatR;

namespace HealLink.Contracts.Doctors;

public class GetSubscriptionsHistoryQueryHandler 
    (ISubscriptionRepository subscriptionRepository): IRequestHandler<GetSubscriptionHistoryQuery, ErrorOr<IEnumerable<SubscriptionHistoryResponse>>>
{
    public async Task<ErrorOr<IEnumerable<SubscriptionHistoryResponse>>> Handle(GetSubscriptionHistoryQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await subscriptionRepository.GetDoctorHistoryByIdAsync(request.DoctorId);

        if (subscriptions == null || !subscriptions.Any())
            return Error.NotFound(description: "there is no subscriptions with the Sended Doctor Id");

        var result= subscriptions.ToSubscriptionHistoryResponse();


        return result.ToErrorOr();
    }


}
