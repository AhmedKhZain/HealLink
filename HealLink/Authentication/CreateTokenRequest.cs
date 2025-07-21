namespace HealLink.Api.Authentication;

public record CreateTokenRequest(
    string Email,
    string Type);

