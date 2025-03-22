using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        public string? Password { get; set; }
    }
}
