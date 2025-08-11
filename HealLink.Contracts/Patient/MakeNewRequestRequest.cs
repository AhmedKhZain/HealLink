namespace HealLink.Contracts.Patient;

public record MakeRequestRequest
    (Guid PatientId,
    Guid DoctorId,
    string plan,
    string PaymentId,
    string? RequestTypename,
    Guid? subscriptionId,
    string? FileLink);