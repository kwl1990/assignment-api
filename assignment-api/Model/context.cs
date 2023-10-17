using Microsoft.EntityFrameworkCore;

namespace assignment_api.Model
{
    public class context : DbContext
    {
        public DbSet<friends> Friends { get; set; }

        public context(DbContextOptions options) : base(options)
        { 
        }
    }
}
