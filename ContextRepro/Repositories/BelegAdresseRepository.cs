using ContextRepro.Entity.Belege;

namespace ContextRepro.Repositories
{
    public class BelegAdresseRepository : DbContextRepository<BelegAdresse, Context>
    {
        public BelegAdresseRepository(Context context) : base(context)
        {
        }
    }
}
