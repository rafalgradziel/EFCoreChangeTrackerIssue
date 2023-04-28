using ContextRepro.Entity.Belege;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextRepro.Entity.Configuration
{
    public class KontaktConfiguration : IEntityTypeConfiguration<Kontakt>
    {
        public void Configure(EntityTypeBuilder<Kontakt> entity)
        {
            entity.HasKey(e => e.KontaktId).HasName("PK_dbo.Kontakt");
            entity.Property(e => e.KontaktId).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(e => e.KontaktGuid).HasDefaultValueSql("('00000000-0000-0000-0000-000000000000')");
        }
    }
}
