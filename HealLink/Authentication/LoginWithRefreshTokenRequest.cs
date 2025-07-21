using Org.BouncyCastle.Bcpg;

namespace HealLink.Api.Authentication;

public record LoginWithRefreshTokenRequest(
    string RefreshToken,
    Guid UserId);
