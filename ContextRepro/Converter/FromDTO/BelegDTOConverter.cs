using ContextRepro.DTOs;
using ContextRepro.Entity.Belege;

namespace ContextRepro.Converter.FromDTO
{
    public class BelegDTOConverter
    {
        public Beleg CreateOrUpdateFromDTO(Context db, Vorgang vorgang, BelegDTO dtoBeleg)
        {
            if (dtoBeleg == null) return null;
            if (dtoBeleg.BelegGuid == Guid.Empty)
                throw new ArgumentException("BelegGuid is required");

            Beleg beleg = vorgang.Belege.FirstOrDefault(b => b.BelegGuid == dtoBeleg.BelegGuid);
            if (beleg == null)
            {
                beleg = db.Belege.CreateInstance();
                vorgang.Belege.Add(beleg);
            }

            beleg.BelegGuid = dtoBeleg.BelegGuid;
            beleg.SetBelegAdresse(BelegAdresseDTOConverter.CreateOrUpdateFromDTO(db, dtoBeleg.BelegAdresse));
            beleg.SetVersandAdresse(beleg.GetBelegAdresse());
            beleg.SetVersandAdresse(BelegAdresseDTOConverter.CreateOrUpdateFromDTO(db, dtoBeleg.VersandAdresse));

            if (beleg.BelegAdresse != null)
            {
                beleg.BelegAdresse.Kontakt = vorgang.Kontakt;
                beleg.BelegAdresse.KontaktId = vorgang.Kontakt.KontaktId;
            }
            if (beleg.VersandAdresse != null)
            {
                beleg.VersandAdresse.Kontakt = vorgang.Kontakt;
                beleg.VersandAdresse.KontaktId = vorgang.Kontakt.KontaktId;
            }

            return beleg;
        }

    }
}
