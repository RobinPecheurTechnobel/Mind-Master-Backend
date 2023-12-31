using DAL.Entities;
using DAL.Entities.Relations;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MindMasterContext : DbContext
    {
        public MindMasterContext(DbContextOptions options) : base(options) {
            //DbInitializer.Initialize(this);
        }

        public DbSet<ThinkerEntity> Thinkers { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<GroupThinkerEntity> GroupThinkers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
