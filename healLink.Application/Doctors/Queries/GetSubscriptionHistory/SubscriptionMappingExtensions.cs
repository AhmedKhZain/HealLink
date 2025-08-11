using HealLink.Domain.PatientDoctorSubscriptions;

namespace HealLink.Contracts.Doctors;

public static class SubscriptionMappingExtensions
{
    public static IEnumerable<SubscriptionHistoryResponse> ToSubscriptionHistoryResponse(this IEnumerable<PatientDoctorSubscription> subscriptions)
    {
        var result = subscriptions.Select(sub => new SubscriptionHistoryResponse
        (
            PatientId: sub.PatientId,
            PatientName: sub.Patient.User.NameToShow,
            StartDate: sub.SubscribedAt,
            EndDate: sub.ExpiryDate.Value,
            TotalHours: (int)(sub.ExpiryDate.Value - sub.SubscribedAt).TotalHours
        ));

        return result;
    }
}