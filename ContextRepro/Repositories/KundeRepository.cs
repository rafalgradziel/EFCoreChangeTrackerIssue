using ContextRepro.Entity.Belege;

namespace ContextRepro.Repositories
{
    public class KundeRepository : DbContextRepository<Kontakt, Context>
    {
        public KundeRepository(Context context) : base(context)
        {
            Kontakt result = Set.FirstOrDefault(k => k.KundenNummer == "<not assigned>");
            if (result == null)
            {
                result = CreateInstance();
                result.KundenNummer = "<not assigned>";
                Add(result, true);
            }
        }
        
        public Kontakt Get(Guid kundeGuid)
        {
            return Set.FirstOrDefault(k => k.KontaktGuid.Equals(kundeGuid));
        }

        public Kontakt GetDummyKunde()
        {
            return Set.FirstOrDefault(k => k.KundenNummer == "<not assigned>");
        }
    }
}
