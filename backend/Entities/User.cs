using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } // User's name

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public required string Email { get; set; } // Email address, unique

        [Required]
        public required string PasswordHash { get; set; } // Password hash

        public DateTime? LastLogin { get; set; } // Last login time

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow; // Registration time

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "active"; // 'active' or 'blocked'

        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
