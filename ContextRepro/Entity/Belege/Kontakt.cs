namespace ContextRepro.Entity.Belege
{
    public class Kontakt
    {
        public long KontaktId { get; set; }
        public Guid KontaktGuid { get; set; }
        public string KundenNummer { get; set; }

        public Kontakt()
        {
            KontaktGuid = Guid.NewGuid();
        }

        public bool IstDummyKontakt()
        {
            return KundenNummer?.Equals("<not assigned>", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
    }
}
