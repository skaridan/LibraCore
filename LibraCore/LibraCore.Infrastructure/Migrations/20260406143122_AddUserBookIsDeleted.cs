using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBookIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDI23fbkbCm+jX4u5W7m1O9gruik8omVn0ni9J8w7ee86fUfFzYQNLoR15kQjLxKUg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFm9P7KOZAKQ3UYtx6SRnqrYDVWQxnMWdtDPJmcJxlldRWoQYqJpSvarlGCQr6SfAg==");

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3009-4b2f-8a0a-000000000309"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-300a-4b2f-8a0a-00000000030a"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersBooks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENk+iTKgYFpJAJDVuftm2xENoPW1q/ZQoeN8uUHo7tJ+X0Wx/7pKtvlTBCLnx/o5eQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAn1zoprKyZ9jNMaOujCkFE2ittyi5qiRONIbwCpnhEYOffrcvI1EC6BIRAh13yYYA==");
        }
    }
}
