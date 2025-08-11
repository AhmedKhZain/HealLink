namespace HealLink.Contracts.Patient;

public record GetMedicalHistoryRequest
    (Guid UserId,
    string Type,
    int PageNumber = 1,
    int PageSize = 12);
