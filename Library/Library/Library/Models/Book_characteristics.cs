namespace Library.Models
{


    using System.ComponentModel.DataAnnotations;

    public class Book_characteristics
    {
        public int Id { get; set; }  

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [Display(Name = "Author Name")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Range(1500, 2100, ErrorMessage = "Enter a valid publication year")]
        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }

        [Display(Name = "Book Description")]
        public string Description { get; set; }

        [Range(1, 5000, ErrorMessage = "Page count must be a positive number")]
        [Display(Name = "Number of Pages")]
        public int Pages { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Cover image URL is required")]
        [Display(Name = "Cover Image URL")]
        public string CoverImage { get; set; }

        [Required(ErrorMessage = "Book file path is required")]
        [Display(Name = "Book File Path")]
        public string FilePath { get; set; }
    }











}



