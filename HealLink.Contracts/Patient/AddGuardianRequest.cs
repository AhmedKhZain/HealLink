namespace HealLink.Contracts.Patient;
public record AddGuardianRequest(
    Guid PatientId,
    Guid GuardinId,
    string? relationship
);
