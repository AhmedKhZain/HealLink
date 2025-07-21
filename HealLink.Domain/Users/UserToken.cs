using ErrorOr;
using HealLink.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Users
{
    public class UserToken
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid UserId { get; private set; }    
        public User User { get; private set; } = null!; // Navigation property to the User entity
        public TokenTypes Type { get; private set; } = TokenTypes.EmailConfirmation;     

        public string Token { get; private set; } =null!; // This should be set when the token is generated, e.g., by a token generator service
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsUsed { get; private set; } = false; 
        public DateTime? UsedAt { get; private set; } = null; // Nullable to allow for unused tokens



        private UserToken() { } // EF Core requires a parameterless constructor


        public UserToken(Guid userId, 
            TokenTypes? type, 
            string token,
            DateTime? expiresAt = null)
        {
            UserId = userId;
            Type = type?? TokenTypes.EmailConfirmation;
            Token = token ?? throw new ArgumentNullException(nameof(token), "Token cannot be null.");
        }

        public bool IsExpired() => CreatedAt.Add(Type.Expiration) < DateTime.UtcNow;



        public void MarkUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow; // Mark the token as used and set the UsedAt timestamp
        }



    }

}
