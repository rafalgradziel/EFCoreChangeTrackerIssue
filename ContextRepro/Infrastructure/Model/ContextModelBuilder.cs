using ContextRepro.Entity.Configuration;
using ContextRepro.Entity.Configuration.Sequence;
using Microsoft.EntityFrameworkCore;

namespace ContextRepro.Infrastructure.Model
{
    public class ContextModelBuilder
    {
        private ModelBuilder _modelBuilder;
        public ContextModelBuilder(ModelBuilder modelBuilder)
        {
            this._modelBuilder = modelBuilder;
        }
        public void ApplyConfigurations()
        {
            _modelBuilder.ApplyConfiguration(new BelegConfiguration());
            _modelBuilder.ApplyConfiguration(new VorgangConfiguration());
            _modelBuilder.ApplyConfiguration(new BelegAdresseConfiguration());
            _modelBuilder.ApplyConfiguration(new KontaktConfiguration());

            // Sequence
            _modelBuilder.ApplyConfiguration(new SequenceConfiguration());
        }
    }
}
