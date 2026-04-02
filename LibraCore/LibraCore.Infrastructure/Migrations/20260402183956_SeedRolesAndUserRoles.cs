using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("e1d8e8b0-4001-4b2f-8a0a-000000000401"), "CONC-ROLE-0001", "Admin", "ADMIN" },
                    { new Guid("e1d8e8b0-4002-4b2f-8a0a-000000000402"), "CONC-ROLE-0002", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKIKkGZz5+zvBj+6f1DLUHR6XHiG0ghn4OCG72HLv5FPDdBWKIH7veylHzZfKPoDhg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEABSMcE+u/NKXW1ocEDmtqz+EvFnA+BfL3NOhDMM2zMfVVFJl7jQAbQ5KDlPCv2bsg==");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("e1d8e8b0-4001-4b2f-8a0a-000000000401"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("e1d8e8b0-4002-4b2f-8a0a-000000000402"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1d8e8b0-4001-4b2f-8a0a-000000000401"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1d8e8b0-4002-4b2f-8a0a-000000000402"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1d8e8b0-4001-4b2f-8a0a-000000000401"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1d8e8b0-4002-4b2f-8a0a-000000000402"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHuGlvUJqBvQ8MDINKSFxAtCd71/e2ATlESz3PAOfUJf0KjIhJWXjhreSd4cnx468w==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEABECB81eY+iz1mtCWLjZTiAdx8hNdb2NM5/gxrQZMi/YNWoK/c+TYnrbK8iQfnvbw==");
        }
    }
}
