using Microsoft.EntityFrameworkCore;

namespace TempFileServer.Models
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<File> Files { get; set; }
    }
}