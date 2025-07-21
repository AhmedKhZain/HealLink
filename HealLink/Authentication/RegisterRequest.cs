namespace HealLink.Api.Authentication
{
    public record RegisterRequest(
        string FullName,
        string NameToShow,
        string Email,
        string Password);
 
}
