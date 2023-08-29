using Microsoft.EntityFrameworkCore;

namespace ForumSystemApi1.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
