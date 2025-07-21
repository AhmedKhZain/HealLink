using HealLink.Domain.Users;

namespace HealLink.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);