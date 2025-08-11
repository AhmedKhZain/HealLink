namespace HealLink.Contracts.Authentication;
public record CreateTokenRequest(
    string Email,
    string Type);

