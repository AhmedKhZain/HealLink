using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Common.Authentication.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {


        private readonly JwtSettings _jwtSettings;

        public TokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }


        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key)); // Replace with your actual secret key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role.ToString()),
                new Claim("name", user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials);



            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public UserToken GenerateUserTokens(User user, TokenTypes tokenType)
        {
            string token;
            DateTime expiration;

            switch (tokenType.Name)
            {
                case "refresh-token":
                    token = GenerateSecureRandomString(128);
                    expiration = DateTime.UtcNow.AddDays(7);
                    break;

                case "email-confirmation":
                    token = GenerateSecureRandomString(6);
                    expiration = DateTime.UtcNow.AddHours(2);
                    break;

                case "password-reset":
                    token = GenerateSecureRandomString(6);
                    expiration = DateTime.UtcNow.AddMinutes(15);
                    break;

                case "2fa":
                    token = GenerateNumericCode(6);
                    expiration = DateTime.UtcNow.AddMinutes(10);
                    break;

                default:
                    throw new ArgumentException("Unsupported token type");
            }

            return new UserToken(user.Id, tokenType, token, expiration);

        }



        public static string GenerateSecureRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }

        private string GenerateNumericCode(int length)
        {
            var rng = new Random();
            return rng.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length)).ToString();
        }
    }
}
