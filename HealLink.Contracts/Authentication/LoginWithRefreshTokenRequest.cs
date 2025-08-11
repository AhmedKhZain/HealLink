
namespace HealLink.Contracts.Authentication;

public record LoginWithRefreshTokenRequest(
    string RefreshToken,
    Guid UserId);
