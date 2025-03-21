using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
    }
}
