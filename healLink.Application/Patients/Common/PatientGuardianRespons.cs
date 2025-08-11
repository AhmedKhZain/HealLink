namespace healLink.Application.Patients.Common
{
    public class PatientGuardianResponse
    {
        public Guid Id { get; init; }
        public Guid GuardianId { get; init; }
        public string GuardianName { get; init; } = string.Empty;
        public string? GuardianPhotoLink { get; init; }
        public Guid PatientId { get; init; }
        public string PatientName { get; init; } = string.Empty;
        public string? PatientPhotoLink { get; init; }
        public string Relationship { get; init; } = string.Empty;
        public DateTime StartedAt { get; init; }
    }
}
