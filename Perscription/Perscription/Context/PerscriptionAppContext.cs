using Microsoft.EntityFrameworkCore;
using Perscription.Models;

namespace Perscription.Context;

public class PerscriptionAppContext : DbContext
{
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

                entity.ToTable("Perscription", "prsp");

                entity.Property(e => e.IdPerscription).ValueGeneratedOnAdd();
                entity.Property(e => e.Date);
                entity.Property(e => e.DueDate);
                entity.HasOne(e => e.Doctor)
                    .WithMany(c => c.Perscriptions)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            }
        );
        
        modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("PK_Doctor");

                entity.ToTable("Doctor", "prsp");

                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
            }
        );
    }
}