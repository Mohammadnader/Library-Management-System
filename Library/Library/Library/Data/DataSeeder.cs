using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Library.Data
{
    public static class DataSeeder
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_characteristics>().HasData(
                new Book_characteristics
                {
                    Id = 1,
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Author = "Douglas Adams",
                    Category = "Science Fiction",
                    YearPublished = 1979,
                    Description = "A comedic science fiction series created by Douglas Adams. Originally a radio comedy broadcast on BBC Radio 4 in 1978, it was later adapted to other formats, including stage shows, novels, comic books, a 1981 TV series, a 1984 video game, and a 2005 feature film.",
                    Pages = 193,
                    Language = "English",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/en/b/bd/Hhgttg.jpg",
                    FilePath = "/books/hitchhikers_guide.pdf"
                },
                new Book_characteristics
                {
                    Id = 2,
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Category = "Fantasy",
                    YearPublished = 1954,
                    Description = "An epic high-fantasy novel written by English author and scholar J. R. R. Tolkien. The story began as a sequel to Tolkien's 1937 fantasy novel The Hobbit, but eventually developed into a much larger work.",
                    Pages = 1178,
                    Language = "English",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif",
                    FilePath = "/books/lord_of_the_rings.pdf"
                },
                new Book_characteristics
                {
                    Id = 3,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Category = "Romance",
                    YearPublished = 1813,
                    Description = "A romantic novel of manners written by Jane Austen in 1813. The novel follows the character development of Elizabeth Bennet, the dynamic protagonist of the book who learns about the repercussions of hasty judgments and comes to appreciate the difference between superficial goodness and actual goodness.",
                    Pages = 279,
                    Language = "English",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/commons/1/17/PrideAndPrejudiceTitlePage.jpg",
                    FilePath = "/books/pride_and_prejudice.pdf"
                },
                new Book_characteristics
                {
                    Id = 4,
                    Title = "1984",
                    Author = "George Orwell",
                    Category = "Dystopian",
                    YearPublished = 1949,
                    Description = "A dystopian social science fiction novel and cautionary tale by English author George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime.",
                    Pages = 328,
                    Language = "English",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984_first_edition_cover.jpg",
                    FilePath = "/books/1984.pdf"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "admin123", // In a real application, hash passwords!
                    Birthday = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    Role = UserRoles.ADMIN
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Email = "user@example.com",
                    Password = "user123", // In a real application, hash passwords!
                    Birthday = new DateTime(1995, 5, 10),
                    Gender = "Female",
                    Role = UserRoles.USER
                }
            );
        }
    }
}


