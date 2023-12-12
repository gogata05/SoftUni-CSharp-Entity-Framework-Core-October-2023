using Medicines.Data.Models;

namespace Medicines.Data
{
    using Microsoft.EntityFrameworkCore;
    public class MedicinesContext : DbContext
    {
        public MedicinesContext()
        {
        }

        public MedicinesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicine> PatientsMedicines { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicine>
            (e =>
            {
                e.HasKey(x => new { x.PatientId, x.MedicineId });

                // e
                //.HasOne(x => x.Prisoner)
                //.WithMany(y => y.ManyToManyClassPrisonerOfficers)
                //.HasForeignKey(x => x.FKTableKeysNameClientId)
                //.OnDelete(DeleteBehavior.Restrict);
            });  


        }
    }
}
