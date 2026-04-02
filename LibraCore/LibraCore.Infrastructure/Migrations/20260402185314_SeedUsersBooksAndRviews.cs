using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersBooksAndRviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELmFf64vJ3yV6wgxhKTBHUaCK/kH34FUCiDpFWbjUxgKZw3XDjd+e/YiZhwAgznT6g==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEB9LGEIAUqyiczYSFTVsHcV03w5uwTMEL+D+IC0VW0VX4GZ2zdWEwEKPbZ7Yd9/XXQ==");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "IsDeleted", "Rating", "UserId" },
                values: new object[,]
                {
                    { new Guid("f1d8e8b0-5001-4b2f-8a0a-000000000501"), new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"), "A timeless classic — thoroughly enjoyed it.", false, 5, new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("f1d8e8b0-5002-4b2f-8a0a-000000000502"), new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), "Well written with memorable characters.", false, 4, new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("f1d8e8b0-5003-4b2f-8a0a-000000000503"), new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"), "A brilliant and thought‑provoking dystopia.", false, 5, new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("f1d8e8b0-5004-4b2f-8a0a-000000000504"), new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"), "Good ideas but the pacing felt slow to me.", false, 3, new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("f1d8e8b0-5005-4b2f-8a0a-000000000505"), new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"), "Magical, enchanting — perfect for all ages.", false, 5, new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("f1d8e8b0-5006-4b2f-8a0a-000000000506"), new Guid("d1b8e8b0-3008-4b2f-8a0a-000000000308"), "Suspenseful and eerie — kept me up at night.", false, 4, new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") }
                });

            migrationBuilder.InsertData(
                table: "UsersBooks",
                columns: new[] { "BookId", "UserId" },
                values: new object[,]
                {
                    { new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") },
                    { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("d1b8e8b0-3009-4b2f-8a0a-000000000309"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") },
                    { new Guid("d1b8e8b0-300a-4b2f-8a0a-00000000030a"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5001-4b2f-8a0a-000000000501"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5002-4b2f-8a0a-000000000502"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5003-4b2f-8a0a-000000000503"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5004-4b2f-8a0a-000000000504"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5005-4b2f-8a0a-000000000505"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f1d8e8b0-5006-4b2f-8a0a-000000000506"));

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"), new Guid("c1d8e8b0-2001-4b2f-8a0a-000000000201") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-3009-4b2f-8a0a-000000000309"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") });

            migrationBuilder.DeleteData(
                table: "UsersBooks",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { new Guid("d1b8e8b0-300a-4b2f-8a0a-00000000030a"), new Guid("c1d8e8b0-2002-4b2f-8a0a-000000000202") });

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
        }
    }
}
