namespace HealLink.Api.Authentication
{
    public record AuthResponse(
        string FullName,
        string NameToShow,
        string Email,
        string Token);

}
