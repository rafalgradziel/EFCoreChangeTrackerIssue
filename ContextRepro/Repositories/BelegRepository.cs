using ContextRepro.Entity.Belege;

namespace ContextRepro.Repositories
{
    public class BelegRepository : DbContextRepository<Beleg, Context>
    {
        public BelegRepository(Context context) : base(context)
        {
        }
    }
}