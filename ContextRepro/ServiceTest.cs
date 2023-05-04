using ContextRepro.DTOs;
using ContextRepro.Entity.Belege;
using ContextRepro.Factory;
using ContextRepro.Repositories;
using System.Diagnostics;

namespace ContextRepro
{
    public class ServiceTest
    {
        private Context _context;

        public ServiceTest(IContextFactory factory)
        {
            _context = factory.GetContext();
        }

        public void ScenarioOne()
        {
            // prepare
            var data = new VorgangDTO()
            {
                Belege = new List<BelegDTO>()
                {
                    new BelegDTO()
                    {
                        BelegGuid = Guid.NewGuid(),
                        BelegAdresse = new BelegAdresseDTO() { Name = "AAA" },
                        VersandAdresse = new BelegAdresseDTO() { Name = "BBB" },
                    }
                },
                VorgangGuid = Guid.NewGuid(),
            };

            // save
            Vorgang dbVorgang = new Converter.FromDTO.VorgangDTOConverter().CreateOrUpdateFromDTO(_context, data);

            // asserts
            Debug.Assert(dbVorgang.Belege[0].BelegAdresse != null, "Expecting Belege.BelegAdresse not NULL");
            Debug.Assert(dbVorgang.Belege[0].VersandAdresse != null, "Expecting Belege.VersandAdresse not NULL");
        }

        public void ScenarioTwo()
        {
            using (var kundeRepo = new KundeRepository(_context))
            using (var vorgangRepo = new VorgangRepository(_context))
            {
                // prepare
                var kontakt = kundeRepo.GetDummyKunde();

                var vorgang = vorgangRepo.Create();
                vorgang.Kontakt = kontakt;

                var beleg = _context.Belege.CreateInstance();
                beleg.Vorgang = vorgang;

                var ba1 = _context.BelegAdressen.CreateInstance();
                ba1.Name = "AAA";
                ba1.Kontakt = kontakt;
                var ba2 = _context.BelegAdressen.CreateInstance();
                ba2.Name = "ZZZ";
                ba2.Kontakt = kontakt;

                beleg.BelegAdresse = ba1;
                beleg.VersandAdresse = ba2;

                // save
                vorgangRepo.Add(vorgang);

                // asserts
                Debug.Assert(vorgang.Belege[0].BelegAdresse != null, "Expecting Belege.BelegAdresse not NULL");
                Debug.Assert(vorgang.Belege[0].VersandAdresse != null, "Expecting Belege.VersandAdresse not NULL");
            }
        }
    }
}
