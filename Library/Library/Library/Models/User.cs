using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public string Role { get; set; } = "USER"; // Default role is USER

        // Deprecated: Use Role property instead
        public string? Key { get; set; }
    }

    public static class UserRoles
    {
        public const string ADMIN = "ADMIN";
        public const string USER = "USER";
    }
}
