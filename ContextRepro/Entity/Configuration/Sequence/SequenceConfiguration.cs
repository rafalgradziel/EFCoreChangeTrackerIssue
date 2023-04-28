using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextRepro.Entity.Configuration.Sequence
{
    public class SequenceConfiguration: IEntityTypeConfiguration<Entity.Sequence.Sequence>
    {
        public void Configure(EntityTypeBuilder<Entity.Sequence.Sequence> entity)
        {
            entity.ToTable("Sequence");
            entity.HasKey(e => e.Id).HasName("PK_dbo.Sequence");
            entity.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(e => e.TableName).HasMaxLength(80).IsRequired();
            entity.Property(e => e.LastId).IsRequired();
        }
    }
}
