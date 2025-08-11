using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HealLinkDb_V21_FirstLunchMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DoctorRequestId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "SubscriptionChatMessages");

            migrationBuilder.RenameColumn(
                name: "PaidAt",
                table: "Payments",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentLink",
                table: "SubscriptionChatMessages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "MassageDate",
                table: "SubscriptionChatMessages",
                type: "Date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "MassageTime",
                table: "SubscriptionChatMessages",
                type: "Time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorRequestId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "PatientGuardians",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GuardianId",
                table: "PatientGuardians",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MedicalHistories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "RefundItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentProviderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RefundDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundItems_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionChatMessages_MassageDate",
                table: "SubscriptionChatMessages",
                column: "MassageDate");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DoctorRequestId",
                table: "Payments",
                column: "DoctorRequestId",
                unique: true,
                filter: "[DoctorRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_Type",
                table: "MedicalHistories",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_RefundItems_PaymentId",
                table: "RefundItems",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundItems");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionChatMessages_MassageDate",
                table: "SubscriptionChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DoctorRequestId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_Type",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "AttachmentLink",
                table: "SubscriptionChatMessages");

            migrationBuilder.DropColumn(
                name: "MassageDate",
                table: "SubscriptionChatMessages");

            migrationBuilder.DropColumn(
                name: "MassageTime",
                table: "SubscriptionChatMessages");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Payments",
                newName: "PaidAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "SubscriptionChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorRequestId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "PatientGuardians",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GuardianId",
                table: "PatientGuardians",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesPerDay = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DoctorRequestId",
                table: "Payments",
                column: "DoctorRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PrescriptionId",
                table: "Medications",
                column: "PrescriptionId");
        }
    }
}
