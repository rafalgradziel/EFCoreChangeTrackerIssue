using ContextRepro.DTOs;
using ContextRepro.Entity.Belege;

namespace ContextRepro.Converter.FromDTO
{
    public class BelegAdresseDTOConverter
    {
        public static BelegAdresse CreateOrUpdateFromDTO(Context db, BelegAdresseDTO dtoBelegAdresse)
        {
            if (dtoBelegAdresse == null)
                throw new ArgumentException("Cannot be empty!");
            if (dtoBelegAdresse.AdressGuid == Guid.Empty)
                throw new ArgumentException("AdressGuid cannot be empty!");

            BelegAdresse adresse = db.BelegAdressen.FirstOrDefault(a => a.AdressGuid == dtoBelegAdresse.AdressGuid);
            if (adresse == null)
            {
                adresse = db.BelegAdressen.CreateInstance();
            }
            adresse.AdressGuid = dtoBelegAdresse.AdressGuid;
            adresse.Name = dtoBelegAdresse.Name;
            return adresse;
        }
    }
}
