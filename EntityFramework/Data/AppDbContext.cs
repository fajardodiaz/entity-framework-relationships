using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Faction> Factions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
