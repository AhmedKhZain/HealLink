namespace HealLink.Api.Authentication;

public record ResetPasswordRequest(
    string Email,
    string Token,
    string NewPassword);


