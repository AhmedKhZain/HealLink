namespace HealLink.Contracts.Authentication;

public record ConfirmEmailRequest(
    string Email,
    string Token,
    Guid UserId);


