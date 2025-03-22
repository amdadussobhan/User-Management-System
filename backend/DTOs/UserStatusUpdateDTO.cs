namespace backend.DTOs
{
    public class UserStatusUpdateDTO
    {
        public int UserId { get; set; }
        public required string Status { get; set; }
    }
}
