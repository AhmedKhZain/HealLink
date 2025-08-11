namespace HealLink.Contracts.Patient;

public record GetPatientRequest(Guid PatientId,
        bool newestFirst = true);

