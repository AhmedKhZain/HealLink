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
    public bool Is2FAEnabled { get; private set; } = false;

    public ICollection<UserToken> Tokens = new List<UserToken>();


    private User() { }

    public User(string fullName,
        string nameToShow,
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

    public void ChangePhoto(string newprofilePhotolink)
    {
        ProfilePhotoLink = newprofilePhotolink;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public ErrorOr<Success> ChangeRole(Role newRole)
    {
        if (Role.Name == newRole.Name)
            return Error.Custom(code: "DoneBefore", description: "The role is already assingd to the user.",type:3);
        Role = newRole;
        return Result.Success;
    }
    public void UpdateProfile(
        string? showName,
        string? fullName,
        string? photoPath,
        string? email)
    {
        if (showName is not null)
            NameToShow = showName;

        if (fullName is not null)
            FullName = fullName;

        if (photoPath is not null)
            ProfilePhotoLink = photoPath;

        if (email is not null)
        {
            Email = email;
            IsVerified = false; 
        }
    }


    public ErrorOr<Success> UpdatePassword(string newPasswordHash, IPasswordHasher _passwordHasher)
    {
        var result = _passwordHasher.HashPassword(newPasswordHash);
        if (result.IsError)
        {
            return result.Errors;
        }
        _passwordHash = result.Value;
        return Result.Success;
    }

    public void ConfirmEmail()
    {
        IsVerified = true;
    }
}
