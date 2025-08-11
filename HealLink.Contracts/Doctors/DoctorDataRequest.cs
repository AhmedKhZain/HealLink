namespace HealLink.Contracts.Doctors;

public record DoctorDataRequest(Guid DoctorId, string? syndicateIdImageLink, string? nationalId, string? licenseNumber);