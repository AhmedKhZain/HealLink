namespace HealLink.Application.Common.Models;

public record CurrentUser(
    Guid Id,
    string Role);