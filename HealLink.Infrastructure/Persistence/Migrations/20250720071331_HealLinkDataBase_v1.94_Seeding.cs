using HealLink.Domain.Admins;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Persistence.Migrations
{
    public partial class HealLinkDataBase_v194_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 👇 استخدم Constructor بتاع الـ User
            var user = new User(
                fullName: "Ahmed Kh. Zain",
                nameToShow: "A. Zain",
                passwordHash: "blablablabala", // يفضل تبقى مشفّرة
                email: "eltwoo3m@gmail.com",
                role: Role.Admin,
                id:Guid.Parse("D7BAB3C1-9F30-4B17-947A-532F5C9EC2CD")
            );

            // 👇 Admin بياخد الـ Id بتاع الـ User
            var admin = new Admin(user.Id);

            // Insert into Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id",
                    "FullName",
                    "NameToShow",
                    "Email",
                    "PasswordHash",
                    "Role",
                    "IsVerified",
                    "CreatedAt",
                    "ProfilePhotoLink"
                },
                values: new object[]
                {
                    user.Id,
                    user.FullName,
                    user.NameToShow,
                    user.Email,
                    user._passwordHash,       // لو private اعمل accessor ليه
                    user.Role.Name,
                    user.IsVerified,
                    user.CreatedAt,
                    user.ProfilePhotoLink
                });

            // Insert into Admins
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id" , "CreatedAt" },
                values: new object[] { admin.Id, DateTime.UtcNow });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // متأكد إنك لازم تستخدم نفس الـ ID
            var userId = new Guid("D7BAB3C1-9F30-4B17-947A-532F5C9EC2CD");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: userId
            );

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: userId
            );
        }
    }
}
