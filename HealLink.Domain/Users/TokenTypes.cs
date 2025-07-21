using Ardalis.SmartEnum;

namespace HealLink.Domain.Users
{
    public class TokenTypes : SmartEnum<TokenTypes>
    {
        public TimeSpan Expiration { get; }
        public string TokenName { get; }

        private TokenTypes(string name, int value, TimeSpan expiration, string tokenName) : base(name, value)
        {
            Expiration = expiration;
            TokenName = tokenName;
        }

        public static readonly TokenTypes EmailConfirmation =
            new TokenTypes("email-confirmation", 1, TimeSpan.FromHours(24), "Email Confirmation");

        public static readonly TokenTypes PasswordReset =
            new TokenTypes("password-reset", 2, TimeSpan.FromMinutes(15), "Password Reset");

        public static readonly TokenTypes RefreshToken =
            new TokenTypes("refresh-token", 3, TimeSpan.FromDays(7), "Refresh Token");

        public static readonly TokenTypes TwoFactorAuthentication =
            new TokenTypes("2fa", 4, TimeSpan.FromMinutes(5), "Two Factor Authentication");
    }


}
