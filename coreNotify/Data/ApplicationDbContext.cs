using coreNotify.Models;
using Microsoft.EntityFrameworkCore;

namespace coreNotify.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<coreNotify.Models.Application> Application { get; set; } = default!;
    }
}
