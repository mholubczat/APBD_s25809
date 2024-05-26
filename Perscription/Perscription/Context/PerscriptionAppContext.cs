using Microsoft.EntityFrameworkCore;

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

                entity.ToTable("Perscription", "trip");

                entity.Property(e => e.IdPerscription).ValueGeneratedOnAdd();
                entity.Property(e => e.Date);
                entity.Property(e => e.DueDate);
                entity.HasOne(e => e.Doctor).WithMany(e => e.Perscriptions);
            }
        );
    }
}