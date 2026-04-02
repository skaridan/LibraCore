using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthorGenreAndApplicationUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"), 0, "CONC-00000001", "admin@libra.local", true, false, null, "ADMIN@LIBRA.LOCAL", "ADMIN@LIBRA.LOCAL", "AQAAAAIAAYagAAAAEKjN7j2ZtxochN+mtOPtBR4LK0g3ORE5+xRTwfu8QpHUmIFwR9aLMZpY4rAvAGsMKw==", null, false, "A1D8E8B0-2001-4B2F-8A0A-000000000201", false, "admin@libra.local" },
                    { new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"), 0, "CONC-00000002", "user@libra.local", true, false, null, "USER@LIBRA.LOCAL", "USER@LIBRA.LOCAL", "AQAAAAIAAYagAAAAEI+LOo2C1b6jJ2aaIslMpkyTI3jGxMorfDLd8F8CshNatDHbHwStqlOF30fKIiWe/A==", null, false, "A1D8E8B0-2002-4B2F-8A0A-000000000202", false, "user@libra.local" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b1d8e8b0-1001-4b2f-8a0a-000000000101"), "Jane Austen" },
                    { new Guid("b1d8e8b0-1002-4b2f-8a0a-000000000102"), "Charles Dickens" },
                    { new Guid("b1d8e8b0-1003-4b2f-8a0a-000000000103"), "Mark Twain" },
                    { new Guid("b1d8e8b0-1004-4b2f-8a0a-000000000104"), "George Orwell" },
                    { new Guid("b1d8e8b0-1005-4b2f-8a0a-000000000105"), "Virginia Woolf" },
                    { new Guid("b1d8e8b0-1006-4b2f-8a0a-000000000106"), "F. Scott Fitzgerald" },
                    { new Guid("b1d8e8b0-1007-4b2f-8a0a-000000000107"), "J.K. Rowling" },
                    { new Guid("b1d8e8b0-1008-4b2f-8a0a-000000000108"), "Stephen King" },
                    { new Guid("b1d8e8b0-1009-4b2f-8a0a-000000000109"), "Agatha Christie" },
                    { new Guid("b1d8e8b0-100a-4b2f-8a0a-00000000010a"), "J.R.R. Tolkien" },
                    { new Guid("b1d8e8b0-100b-4b2f-8a0a-00000000010b"), "Ernest Hemingway" },
                    { new Guid("b1d8e8b0-100c-4b2f-8a0a-00000000010c"), "Leo Tolstoy" },
                    { new Guid("b1d8e8b0-100d-4b2f-8a0a-00000000010d"), "Isabel Allende" },
                    { new Guid("b1d8e8b0-100e-4b2f-8a0a-00000000010e"), "Toni Morrison" },
                    { new Guid("b1d8e8b0-100f-4b2f-8a0a-00000000010f"), "Haruki Murakami" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"), "Fiction" },
                    { new Guid("a1f8e8b0-0002-4b2f-8a0a-000000000002"), "Non-Fiction" },
                    { new Guid("a1f8e8b0-0003-4b2f-8a0a-000000000003"), "Mystery" },
                    { new Guid("a1f8e8b0-0004-4b2f-8a0a-000000000004"), "Thriller" },
                    { new Guid("a1f8e8b0-0005-4b2f-8a0a-000000000005"), "Romance" },
                    { new Guid("a1f8e8b0-0006-4b2f-8a0a-000000000006"), "Fantasy" },
                    { new Guid("a1f8e8b0-0007-4b2f-8a0a-000000000007"), "Science Fiction" },
                    { new Guid("a1f8e8b0-0008-4b2f-8a0a-000000000008"), "Biography" },
                    { new Guid("a1f8e8b0-0009-4b2f-8a0a-000000000009"), "History" },
                    { new Guid("a1f8e8b0-000a-4b2f-8a0a-00000000000a"), "Children's" },
                    { new Guid("a1f8e8b0-000b-4b2f-8a0a-00000000000b"), "Young Adult" },
                    { new Guid("a1f8e8b0-000c-4b2f-8a0a-00000000000c"), "Horror" },
                    { new Guid("a1f8e8b0-000d-4b2f-8a0a-00000000000d"), "Poetry" },
                    { new Guid("a1f8e8b0-000e-4b2f-8a0a-00000000000e"), "Self-Help" },
                    { new Guid("a1f8e8b0-000f-4b2f-8a0a-00000000000f"), "Graphic Novel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1001-4b2f-8a0a-000000000101"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1002-4b2f-8a0a-000000000102"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1003-4b2f-8a0a-000000000103"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1004-4b2f-8a0a-000000000104"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1005-4b2f-8a0a-000000000105"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1006-4b2f-8a0a-000000000106"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1007-4b2f-8a0a-000000000107"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1008-4b2f-8a0a-000000000108"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1009-4b2f-8a0a-000000000109"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100a-4b2f-8a0a-00000000010a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100b-4b2f-8a0a-00000000010b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100c-4b2f-8a0a-00000000010c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100d-4b2f-8a0a-00000000010d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100e-4b2f-8a0a-00000000010e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100f-4b2f-8a0a-00000000010f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0002-4b2f-8a0a-000000000002"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0003-4b2f-8a0a-000000000003"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0004-4b2f-8a0a-000000000004"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0005-4b2f-8a0a-000000000005"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0006-4b2f-8a0a-000000000006"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0007-4b2f-8a0a-000000000007"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0008-4b2f-8a0a-000000000008"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0009-4b2f-8a0a-000000000009"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000a-4b2f-8a0a-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000b-4b2f-8a0a-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000c-4b2f-8a0a-00000000000c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000d-4b2f-8a0a-00000000000d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000e-4b2f-8a0a-00000000000e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000f-4b2f-8a0a-00000000000f"));
        }
    }
}
