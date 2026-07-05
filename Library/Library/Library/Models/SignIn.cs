using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class SignIn
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
