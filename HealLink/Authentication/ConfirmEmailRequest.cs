namespace HealLink.Api.Authentication;

public record ConfirmEmailRequest(
    string Email,
    string Token,
    Guid UserId);


