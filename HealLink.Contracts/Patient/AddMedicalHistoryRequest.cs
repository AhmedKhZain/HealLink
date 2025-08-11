namespace HealLink.Contracts.Patient;

public record  AddMedicalHistoryRequest(
    Guid PatientId,
    string FileLink,
    string Description,
    string Type
);