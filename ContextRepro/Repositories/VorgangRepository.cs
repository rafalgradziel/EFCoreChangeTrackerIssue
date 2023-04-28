using ContextRepro.Entity.Belege;
using Microsoft.EntityFrameworkCore;

namespace ContextRepro.Repositories
{
    public class VorgangRepository : DbContextRepository<Vorgang, Context>
    {
        public VorgangRepository(Context context) : base(context)
        {
        }

        public virtual Vorgang Create()
        {
            Vorgang v = Set.CreateInstance();
            return v;
        }

        public override Vorgang Get(long vorgangsnummer)
        {
            Vorgang v = Context.Vorgaenge.FirstOrDefault(vv => vv.VorgangId == vorgangsnummer);
            return v;
        }

        public Vorgang GetOrCreate(Guid vorgangGuid)
        {
            return Get(vorgangGuid) ?? Create();
        }

        public Vorgang Get(Guid vorgangGuid)
        {
            Vorgang v = Context.Vorgaenge.Include("Belege").Include("Belege.BelegAdresse").Include("Belege.VersandAdresse").FirstOrDefault(vv => vv.VorgangGuid == vorgangGuid);
            return v;
        }

        protected override void OnBeforeSave(Vorgang entity)
        {
            using (KundeRepository kr = new KundeRepository(Context))
            {
                if (entity.KontaktId > 0) entity.Kontakt = kr.Get(entity.KontaktId);
                if (entity.Kontakt == null)
                    entity.Kontakt = kr.GetDummyKunde();
            }
        }
    }
}
