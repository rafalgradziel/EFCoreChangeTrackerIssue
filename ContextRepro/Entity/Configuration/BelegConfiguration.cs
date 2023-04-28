using ContextRepro.Entity.Belege;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextRepro.Entity.Configuration
{
    public class BelegConfiguration : IEntityTypeConfiguration<Beleg>
    {
        public void Configure(EntityTypeBuilder<Beleg> entity)
        {
            entity.ToTable("Beleg");
            entity.HasIndex(e => e.VorgangId).HasDatabaseName("IX_VorgangId");
            entity.HasIndex(d => d.BelegAdresse_AdressId).HasDatabaseName("IX_BelegAdresse_AdressId").IsUnique(false);
            entity.HasIndex(d => d.VersandAdresse_AdressId).HasDatabaseName("IX_VersandAdresse_AdressId").IsUnique(false);
            entity.Property(e => e.BelegId).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.HasKey(e => e.BelegId).HasName("PK_dbo.Beleg");

            entity.HasOne(d => d.Vorgang)
                .WithMany(p => p.Belege)
                .HasForeignKey(d => d.VorgangId)
                .HasConstraintName("FK_Beleg_dbo.Vorgang_VorgangId");
            entity.HasOne(d => d.BelegAdresse)
                .WithOne()
                .HasForeignKey<Beleg>(d => d.BelegAdresse_AdressId)
                .HasConstraintName("FK_Beleg_dbo.BelegAdresse_BelegAdresse_AdressId");
            entity.HasOne(d => d.VersandAdresse)
                .WithOne()
                .HasForeignKey<Beleg>(d => d.VersandAdresse_AdressId)
                .HasConstraintName("FK_Beleg_dbo.BelegAdresse_VersandAdresse_AdressId");
        }
    }
}
