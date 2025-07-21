using ErrorOr;
using HealLink.Domain.Common;
using System.Data;

namespace HealLink.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = null!;
    public string NameToShow { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool IsVerified { get; private set; } = false;

    public string _passwordHash = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public string? ProfilePhotoLink = null!;
    public Role Role { get; private set; } = Role.Patient;

    public ICollection<UserToken> Tokens = new List<UserToken>();


    private User() { }

    public User(string fullName,string nameToShow,
        string email,
        string passwordHash, 
        Role? role = null,
        Guid? id = null, 
        bool isVerified = false, 
        string? profilePhotolink = null)
    {
        Id = id ?? Guid.NewGuid();
        FullName = fullName;
        NameToShow = nameToShow;
        Email = email;
        _passwordHash = passwordHash;
        Role = role ?? Role.Patient;
        IsVerified = isVerified;
        ProfilePhotoLink = profilePhotolink;
    }

    public ErrorOr<Success> ChangePhoto(string newprofilePhotolink)
    {
        if (string.IsNullOrWhiteSpace(newprofilePhotolink))
        {
            return Error.Validation("Photo", "Photo cannot be empty.");
        }
        ProfilePhotoLink = newprofilePhotolink;
        return Result.Success;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public ErrorOr<Success> ChangeRole(Role newRole)
    {
        if (Role == newRole)
            return Error.Custom(code: "DoneBefore", description: "The role is already assingd to the user.",type:3);
        Role = newRole;
        return Result.Success;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        _passwordHash = newPasswordHash;
    }
}
