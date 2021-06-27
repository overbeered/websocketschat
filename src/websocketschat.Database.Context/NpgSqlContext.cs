using Microsoft.EntityFrameworkCore;
using websocketschat.Database.Context.Configuring;
using websocketschat.Database.Models;

namespace websocketschat.Database.Context
{
    /// <summary>
    /// Connects to data storage and gets data.
    /// </summary>
    public class NpgSqlContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public NpgSqlContext(DbContextOptions<NpgSqlContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
