using ConfigApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConfigApi
{
    public class Context : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source=database.db");
            }
        }
    }
}
