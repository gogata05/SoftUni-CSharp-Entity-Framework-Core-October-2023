using Cadastre.Data.Models;

namespace Cadastre.Data
{
    using Microsoft.EntityFrameworkCore;
    public class CadastreContext : DbContext
    {
        public CadastreContext()
        {
            
        }

        public CadastreContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCitizen> PropertiesCitizens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyCitizen>(e =>
            {
                e.HasKey(x => new { x.PropertyId, x.CitizenId });

                // e
                //.HasOne(x => x.Prisoner)
                //.WithMany(y => y.ManyToManyClassPrisonerOfficers)
                //.HasForeignKey(x => x.FKTableKeysNameClientId)
                //.OnDelete(DeleteBehavior.Restrict);
            });  


        }
    }
}
