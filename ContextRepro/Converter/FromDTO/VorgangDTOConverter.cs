using ContextRepro.DTOs;
using ContextRepro.Entity.Belege;
using ContextRepro.Repositories;

namespace ContextRepro.Converter.FromDTO
{
    public class VorgangDTOConverter
    {
        public Vorgang CreateOrUpdateFromDTO(Context db, VorgangDTO dtoVorgang)
        {
            if (dtoVorgang == null)
            {
                return null;
            }

            using (var repo = new VorgangRepository(db))
            using (var kunden = new KundeRepository(db))
            {
                var dbVorgang = repo.GetOrCreate(dtoVorgang.VorgangGuid);

                dbVorgang.Kontakt = kunden.Get(dtoVorgang.KundeGuid) ?? kunden.GetDummyKunde();

                var belege = new List<Beleg>();
                var bconv = new BelegDTOConverter();

                foreach (var beleg in dtoVorgang.Belege)
                {
                    belege.Add(bconv.CreateOrUpdateFromDTO(db, dbVorgang, beleg));
                }

                repo.AddOrUpdate(dbVorgang);

                return dbVorgang;
            }
        }
    }
}
