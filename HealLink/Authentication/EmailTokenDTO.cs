using HealLink.Domain.Users;

namespace HealLink.Api.Authentication;

public record EmailTokenDTO
(
    string Email,
    TokenTypes Type
);
