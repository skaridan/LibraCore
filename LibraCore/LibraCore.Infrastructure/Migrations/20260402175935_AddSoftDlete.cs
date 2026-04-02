using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDlete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAt35eUPZL0wBQ/bpD8m5otpTPYVSTfPavfpPo4gugXO0Surfz3QHWqaX3ARcrcQlA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENoU6lU1HBb7mPKFHK2p5ZLS7ICNtt0QwjfI6FX1dOdBWN1IViSWc8iaweCPQxdbXA==");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1001-4b2f-8a0a-000000000101"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1002-4b2f-8a0a-000000000102"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1003-4b2f-8a0a-000000000103"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1004-4b2f-8a0a-000000000104"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1005-4b2f-8a0a-000000000105"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1006-4b2f-8a0a-000000000106"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1007-4b2f-8a0a-000000000107"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1008-4b2f-8a0a-000000000108"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-1009-4b2f-8a0a-000000000109"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100a-4b2f-8a0a-00000000010a"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100b-4b2f-8a0a-00000000010b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100c-4b2f-8a0a-00000000010c"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100d-4b2f-8a0a-00000000010d"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100e-4b2f-8a0a-00000000010e"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b1d8e8b0-100f-4b2f-8a0a-00000000010f"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0002-4b2f-8a0a-000000000002"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0003-4b2f-8a0a-000000000003"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0004-4b2f-8a0a-000000000004"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0005-4b2f-8a0a-000000000005"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0006-4b2f-8a0a-000000000006"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0007-4b2f-8a0a-000000000007"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0008-4b2f-8a0a-000000000008"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-0009-4b2f-8a0a-000000000009"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000a-4b2f-8a0a-00000000000a"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000b-4b2f-8a0a-00000000000b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000c-4b2f-8a0a-00000000000c"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000d-4b2f-8a0a-00000000000d"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000e-4b2f-8a0a-00000000000e"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a1f8e8b0-000f-4b2f-8a0a-00000000000f"),
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKjN7j2ZtxochN+mtOPtBR4LK0g3ORE5+xRTwfu8QpHUmIFwR9aLMZpY4rAvAGsMKw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEI+LOo2C1b6jJ2aaIslMpkyTI3jGxMorfDLd8F8CshNatDHbHwStqlOF30fKIiWe/A==");
        }
    }
}
