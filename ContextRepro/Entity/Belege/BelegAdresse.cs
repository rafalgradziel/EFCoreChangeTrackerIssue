namespace ContextRepro.Entity.Belege
{
    public class BelegAdresse
    {
        public long AdressId { get; set; }
        public Guid AdressGuid { get; set; }
        public string Name { get; set; }
        public virtual Kontakt Kontakt { get; set; }
        public long KontaktId { get; set; }

        public BelegAdresse()
        {
            AdressGuid = Guid.NewGuid();
        }
    }
}
