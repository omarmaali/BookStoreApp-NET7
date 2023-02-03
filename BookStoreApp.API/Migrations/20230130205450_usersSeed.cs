using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class usersSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06094DFD-7EEB-4D1E-9950-D7D3E6422C0C", null, "Admin", "ADMIN" },
                    { "D2591BF1-96EC-4D9D-BD41-5BB9F6D3EC1A", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "585696FB-C3F4-4219-AF7C-F37588219FF9", 0, "14e2f25f-9f7d-45ef-afb9-d96f7bd2631d", "admin@bookapp.com", false, "System", "Admin", false, null, "ADMIN@BOOKAPP.COM", "ADMIN@BOOKAPP.COM", "AQAAAAIAAYagAAAAEOpJkfUnGGi32oRUTo2DK4X77FSv7uMVLKMdMiRsYCDjVrV51SIJMKWoAMf4l0iAhQ==", null, false, "e3fac958-4c5f-413d-9a5d-1ce80e0326d8", false, "admin@bookapp.com" },
                    { "C15ADE04-7357-457B-B529-A8851A5A73CD", 0, "d4d1bcb3-5cf6-46ee-be04-eee1ecbb8660", "user@bookapp.com", false, "System", "User", false, null, "USER@BOOKAPP.COM", "USER@BOOKAPP.COM", "AQAAAAIAAYagAAAAEAdMXWz4BTHLNTfte9pFqP04YB0z0NyZ0qk0CmUYoZtxdQtzTZ1HL28zqghqedCF2w==", null, false, "3559b77a-2539-44ac-bd99-0115a0b63e65", false, "user@bookapp.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "06094DFD-7EEB-4D1E-9950-D7D3E6422C0C", "585696FB-C3F4-4219-AF7C-F37588219FF9" },
                    { "D2591BF1-96EC-4D9D-BD41-5BB9F6D3EC1A", "C15ADE04-7357-457B-B529-A8851A5A73CD" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "06094DFD-7EEB-4D1E-9950-D7D3E6422C0C", "585696FB-C3F4-4219-AF7C-F37588219FF9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "D2591BF1-96EC-4D9D-BD41-5BB9F6D3EC1A", "C15ADE04-7357-457B-B529-A8851A5A73CD" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06094DFD-7EEB-4D1E-9950-D7D3E6422C0C");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "D2591BF1-96EC-4D9D-BD41-5BB9F6D3EC1A");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "585696FB-C3F4-4219-AF7C-F37588219FF9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C15ADE04-7357-457B-B529-A8851A5A73CD");
        }
    }
}
