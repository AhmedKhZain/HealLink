namespace HealLink.Contracts.Patient;

public record GetPatientGuardianRequest
    (Guid userId, 
    int pageNum = 1, 
    int pageSize = 10, 
    bool newestFirst = true);


