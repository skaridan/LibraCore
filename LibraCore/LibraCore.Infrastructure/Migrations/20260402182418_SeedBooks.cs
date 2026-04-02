using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "GenreID", "ImageUrl", "IsDeleted", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"), new Guid("b1d8e8b0-1001-4b2f-8a0a-000000000101"), "A classic romantic novel about manners and marriage.", new Guid("a1f8e8b0-0005-4b2f-8a0a-000000000005"), "https://s3.amazonaws.com/nightjarprod/content/uploads/sites/192/2025/12/02152113/v5gShop7147X33ytbcC2u05KDuc.jpg", false, 9.99m, new DateOnly(1813, 1, 28), "Pride and Prejudice" },
                    { new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"), new Guid("b1d8e8b0-1002-4b2f-8a0a-000000000102"), "The growth and personal development of an orphan named Pip.", new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"), "https://m.media-amazon.com/images/I/815ZFQcdRcL._AC_UF1000,1000_QL80_.jpg", false, 11.50m, new DateOnly(1861, 8, 1), "Great Expectations" },
                    { new Guid("d1b8e8b0-3003-4b2f-8a0a-000000000303"), new Guid("b1d8e8b0-1003-4b2f-8a0a-000000000103"), "A young boy's adventures along the Mississippi River.", new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"), "https://m.media-amazon.com/images/I/91sBZnKzEfL._AC_UF1000,1000_QL80_.jpg", false, 10.00m, new DateOnly(1884, 12, 10), "Adventures of Huckleberry Finn" },
                    { new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"), new Guid("b1d8e8b0-1004-4b2f-8a0a-000000000104"), "A dystopian novel about surveillance and totalitarianism.", new Guid("a1f8e8b0-0007-4b2f-8a0a-000000000007"), "https://m.media-amazon.com/images/I/71wANojhEKL._AC_UF1000,1000_QL80_.jpg", false, 12.99m, new DateOnly(1949, 6, 8), "1984" },
                    { new Guid("d1b8e8b0-3005-4b2f-8a0a-000000000305"), new Guid("b1d8e8b0-1005-4b2f-8a0a-000000000105"), "A day in the life of Clarissa Dalloway in post‑war London.", new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"), "https://www.book.store.bg/lrgimg/779190437/mrs-dalloway.jpg", false, 8.75m, new DateOnly(1925, 5, 14), "Mrs Dalloway" },
                    { new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"), new Guid("b1d8e8b0-1006-4b2f-8a0a-000000000106"), "A story of the Jazz Age and the decline of the American Dream.", new Guid("a1f8e8b0-0001-4b2f-8a0a-000000000001"), "https://upload.wikimedia.org/wikipedia/commons/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg", false, 9.50m, new DateOnly(1925, 4, 10), "The Great Gatsby" },
                    { new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"), new Guid("b1d8e8b0-1007-4b2f-8a0a-000000000107"), "The first book in the Harry Potter series; a young wizard begins his journey.", new Guid("a1f8e8b0-0006-4b2f-8a0a-000000000006"), "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg", false, 14.99m, new DateOnly(1997, 6, 26), "Harry Potter and the Philosopher's Stone" },
                    { new Guid("d1b8e8b0-3008-4b2f-8a0a-000000000308"), new Guid("b1d8e8b0-1008-4b2f-8a0a-000000000108"), "A chilling horror novel set in an isolated hotel.", new Guid("a1f8e8b0-000c-4b2f-8a0a-00000000000c"), "https://m.media-amazon.com/images/I/91U7HNa2NQL._AC_UF1000,1000_QL80_.jpg", false, 13.50m, new DateOnly(1977, 1, 28), "The Shining" },
                    { new Guid("d1b8e8b0-3009-4b2f-8a0a-000000000309"), new Guid("b1d8e8b0-1009-4b2f-8a0a-000000000109"), "A Hercule Poirot mystery on a snowbound train.", new Guid("a1f8e8b0-0003-4b2f-8a0a-000000000003"), "https://m.media-amazon.com/images/I/81aY+Fk-g8L._UF1000,1000_QL80_.jpg", false, 10.99m, new DateOnly(1934, 1, 1), "Murder on the Orient Express" },
                    { new Guid("d1b8e8b0-300a-4b2f-8a0a-00000000030a"), new Guid("b1d8e8b0-100a-4b2f-8a0a-00000000010a"), "A fantasy adventure that precedes the Lord of the Rings.", new Guid("a1f8e8b0-0006-4b2f-8a0a-000000000006"), "https://m.media-amazon.com/images/I/81uEDUfKBZL.jpg", false, 12.00m, new DateOnly(1937, 9, 21), "The Hobbit" },
                    { new Guid("d1b8e8b0-300b-4b2f-8a0a-00000000030b"), new Guid("b1d8e8b0-100c-4b2f-8a0a-00000000010c"), "An epic novel following several families during the Napoleonic era.", new Guid("a1f8e8b0-0009-4b2f-8a0a-000000000009"), "https://m.media-amazon.com/images/I/81W6BFaJJWL._AC_UF1000,1000_QL80_.jpg", false, 15.00m, new DateOnly(1869, 1, 1), "War and Peace" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3001-4b2f-8a0a-000000000301"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3002-4b2f-8a0a-000000000302"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3003-4b2f-8a0a-000000000303"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3004-4b2f-8a0a-000000000304"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3005-4b2f-8a0a-000000000305"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3006-4b2f-8a0a-000000000306"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3007-4b2f-8a0a-000000000307"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3008-4b2f-8a0a-000000000308"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-3009-4b2f-8a0a-000000000309"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-300a-4b2f-8a0a-00000000030a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d1b8e8b0-300b-4b2f-8a0a-00000000030b"));

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
        }
    }
}
