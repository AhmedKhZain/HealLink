namespace HealLink.Application.Common.Authorization;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    //public string? Permissions { get; set; }
    public string? Role { get; set; }
}