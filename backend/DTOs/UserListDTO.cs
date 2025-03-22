namespace backend.DTOs
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
