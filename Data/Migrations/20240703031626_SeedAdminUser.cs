using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUri",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3dccc66f-a940-4065-b20a-cf27ad9340e8", "3dccc66f-a940-4065-b20a-cf27ad9340e8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "07086e6c-0592-4679-9ed6-e41260738a80", 0, "0cb76b71-8fe8-43e3-a8e5-f8f038017c3d", "test@mail.com", true, false, null, "TEST@MAIL.COM", "TEST@MAIL.COM", "AQAAAAIAAYagAAAAEHWNiesFWRbpn47Pyr/5R/RjyyLxbSvi8QDoos+9gkog7i5SunAdI6I10yT1CuyaCw==", null, false, "4123b3d3-5356-498c-b77a-7a081e9fe162", false, "test@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3dccc66f-a940-4065-b20a-cf27ad9340e8", "07086e6c-0592-4679-9ed6-e41260738a80" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3dccc66f-a940-4065-b20a-cf27ad9340e8", "07086e6c-0592-4679-9ed6-e41260738a80" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dccc66f-a940-4065-b20a-cf27ad9340e8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07086e6c-0592-4679-9ed6-e41260738a80");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUri",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
