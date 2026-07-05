using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "CoverImage", "Description", "FilePath", "Language", "Pages", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "Douglas Adams", "Science Fiction", "https://upload.wikimedia.org/wikipedia/en/b/bd/Hhgttg.jpg", "A comedic science fiction series created by Douglas Adams. Originally a radio comedy broadcast on BBC Radio 4 in 1978, it was later adapted to other formats, including stage shows, novels, comic books, a 1981 TV series, a 1984 video game, and a 2005 feature film.", "/books/hitchhikers_guide.pdf", "English", 193, "The Hitchhiker's Guide to the Galaxy", 1979 },
                    { 2, "J.R.R. Tolkien", "Fantasy", "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif", "An epic high-fantasy novel written by English author and scholar J. R. R. Tolkien. The story began as a sequel to Tolkien's 1937 fantasy novel The Hobbit, but eventually developed into a much larger work.", "/books/lord_of_the_rings.pdf", "English", 1178, "The Lord of the Rings", 1954 },
                    { 3, "Jane Austen", "Romance", "https://upload.wikimedia.org/wikipedia/commons/1/17/PrideAndPrejudiceTitlePage.jpg", "A romantic novel of manners written by Jane Austen in 1813. The novel follows the character development of Elizabeth Bennet, the dynamic protagonist of the book who learns about the repercussions of hasty judgments and comes to appreciate the difference between superficial goodness and actual goodness.", "/books/pride_and_prejudice.pdf", "English", 279, "Pride and Prejudice", 1813 },
                    { 4, "George Orwell", "Dystopian", "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984_first_edition_cover.jpg", "A dystopian social science fiction novel and cautionary tale by English author George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime.", "/books/1984.pdf", "English", 328, "1984", 1949 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "Email", "Gender", "Key", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", null, "admin123", "ADMIN", "admin" },
                    { 2, new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Female", null, "user123", "USER", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
