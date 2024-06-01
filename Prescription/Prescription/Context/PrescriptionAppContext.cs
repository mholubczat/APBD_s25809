using Microsoft.EntityFrameworkCore;
using Prescription.Models;

namespace Prescription.Context;

public class PrescriptionAppContext : DbContext
{
    private const string Schema = "prsp";
    public PrescriptionAppContext()
    {
    }

    public PrescriptionAppContext(DbContextOptions<PrescriptionAppContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Prescription> Prescriptions { get; init; }
    public DbSet<Doctor> Doctors { get; init; }
    public DbSet<Medicament> Medicaments { get; init; }
    public DbSet<Patient> Patients { get; init; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; init; }

        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("PK_Prescription");

                entity.ToTable(nameof(Models.Prescription), Schema);

                entity.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
                entity.Property(e => e.Date);
                entity.Property(e => e.DueDate);
                entity.HasOne(e => e.Doctor)
                    .WithMany(c => c.Prescriptions)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Patient)
                    .WithMany(c => c.Prescriptions)
                    .HasForeignKey(d => d.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            }
        );

        modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("PK_Doctor");

                entity.ToTable(nameof(Doctor), Schema);

                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
            }
        );

        modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("PK_Patient");

                entity.ToTable(nameof(Patient), Schema);

                entity.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.BirthDate);
            }
        );

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(e => new{e.IdPrescription, e.IdMedicament}).HasName("PK_Prescription_Medicament");

                entity.ToTable(nameof(PrescriptionMedicament), Schema);

                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Dose).IsRequired(false);
                entity.Property(e => e.Details).HasMaxLength(100);
                entity.HasOne(e => e.Medicament)
                    .WithMany(c => c.Prescriptions)
                    .HasForeignKey(d => d.IdMedicament)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Prescription)
                    .WithMany(c => c.Medicaments)
                    .HasForeignKey(d => d.IdPrescription)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            }
        );

        modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("PK_Medicament");

                entity.ToTable(nameof(Medicament), Schema);

                entity.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Type).HasMaxLength(100);
            }
        );
    }
}