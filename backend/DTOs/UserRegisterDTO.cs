using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }
    }
}
