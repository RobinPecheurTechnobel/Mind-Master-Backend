using DAL.Entities;
using DAL.Entities.Relations;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MindMasterContext : DbContext
    {
        public MindMasterContext(DbContextOptions options) : base(options) {
            DbInitializer.Initialize(this);
        }

        public DbSet<ThinkerEntity> Thinkers { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<GroupThinkerEntity> GroupThinkers { get; set; }
        public DbSet<IdeaEntity> Ideas { get; set; }
        public DbSet<ConceptEntity> Concepts { get; set; }
        public DbSet<ConceptIdeaEntity> ConceptIdeas { get; set; }
        public DbSet<ConceptGroupEntity> ConceptGroups { get; set; }
        public DbSet<AssemblyEntity> Assemblies { get; set; }
        public DbSet<ConceptAssemblyEntity> ConceptAssemblies { get; set; }
        public DbSet<GroupAssemblyEntity> GroupAssemblies { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<LabelConceptEntity> LabelConcepts { get; set; }
        public DbSet<LabelAssemblyEntity> LabelAssemblies { get; set; }

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
