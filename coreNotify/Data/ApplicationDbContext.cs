using coreNotify.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoreNotify.Areas.Identity.Data;

namespace coreNotify.Data
{
    public class ApplicationDbContext : IdentityDbContext<NotifyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Application> Application { get; set; } = default!;
    }
}
