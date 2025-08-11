namespace HealLink.Contracts.Doctors
{
    public record SubscriptionHistoryResponse
        (Guid PatientId,
        string PatientName,
        DateTime StartDate,
        DateTime EndDate,
        int TotalHours);
}