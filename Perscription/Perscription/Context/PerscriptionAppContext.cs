using Microsoft.EntityFrameworkCore;
using Perscription.Models;

namespace Perscription.Context;

public class PerscriptionAppContext : DbContext
{
    private const string Schema = "prsp";
    public PerscriptionAppContext()
    {
    }

    public PerscriptionAppContext(DbContextOptions<PerscriptionAppContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Perscription> Perscriptions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Perscription>(entity =>
            {
                entity.HasKey(e => e.IdPerscription).HasName("PK_Perscription");

                entity.ToTable(nameof(Models.Perscription), Schema);

                entity.Property(e => e.IdPerscription).ValueGeneratedOnAdd();
                entity.Property(e => e.Date);
                entity.Property(e => e.DueDate);
                entity.HasOne(e => e.Doctor)
                    .WithMany(c => c.Perscriptions)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Patient)
                    .WithMany(c => c.Perscriptions)
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

        modelBuilder.Entity<PerscriptionMedicament>(entity =>
            {
                entity.HasKey(e => new{e.IdPerscription, e.IdMedicament}).HasName("PK_Perscription_Medicament");

                entity.ToTable(nameof(PerscriptionMedicament), Schema);

                entity.Property(e => e.IdPerscription).ValueGeneratedNever();
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Dose).IsRequired(false);
                entity.Property(e => e.Details).HasMaxLength(100);
                entity.HasOne(e => e.Medicament)
                    .WithMany(c => c.Perscriptions)
                    .HasForeignKey(d => d.IdMedicament)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Perscription)
                    .WithMany(c => c.Medicaments)
                    .HasForeignKey(d => d.IdPerscription)
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