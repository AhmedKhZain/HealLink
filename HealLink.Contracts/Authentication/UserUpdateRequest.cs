namespace HealLink.Contracts.Authentication;

public record UpdateUserDataRequest
    (Guid Id,
    string? ShowName,
    string? FullName,
    string? PhotoPath,
    string? Email);