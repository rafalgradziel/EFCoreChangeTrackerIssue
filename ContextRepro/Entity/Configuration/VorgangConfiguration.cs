using ContextRepro.Entity.Belege;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextRepro.Entity.Configuration
{
    public class VorgangConfiguration : IEntityTypeConfiguration<Vorgang>
    {
        public void Configure(EntityTypeBuilder<Vorgang> entity)
        {
            entity.ToTable("Vorgang");
            entity.HasIndex(e => e.KontaktId).HasDatabaseName("IX_KontaktId");
            entity.Property(e => e.VorgangId).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.HasKey(e => e.VorgangId).HasName("PK_dbo.Vorgang");

            entity.HasOne(d => d.Kontakt)
                .WithMany()
                .HasForeignKey(d => d.KontaktId)
                .HasConstraintName("FK_dbo.Vorgang_dbo.Kontakt_KontaktId");
        }
    }
}
