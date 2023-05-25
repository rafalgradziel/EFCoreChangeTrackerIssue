using ContextRepro.Entity.Belege;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextRepro.Entity.Configuration
{
    public class BelegAdresseConfiguration : IEntityTypeConfiguration<BelegAdresse>
    {
        public void Configure(EntityTypeBuilder<BelegAdresse> entity)
        {
            entity.ToTable("BelegAdresse");
            entity.HasKey(e => e.AdressId).HasName("PK_dbo.BelegAdresse");
            entity.Property(e => e.AdressGuid).HasDefaultValueSql("('00000000-0000-0000-0000-000000000000')");
            entity.HasIndex(e => e.KontaktId).HasDatabaseName("IX_KontaktId").IsUnique(false);
            entity.Property(e => e.KontaktId).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Kontakt)
                .WithMany()
                .HasForeignKey(d => d.KontaktId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.BelegAdresse_dbo.Kontakt_KontaktId");
        }
    }
}
