namespace ContextRepro.Entity.Belege
{
    public class Beleg
    {
        public long BelegId { get; set; }
        public Guid BelegGuid { get; set; }
        public long? VorgangId { get; set; }
        public virtual Vorgang Vorgang { get; set; }
        public virtual BelegAdresse BelegAdresse { get; set; }
        public virtual BelegAdresse VersandAdresse { get; set; }
        public long? BelegAdresse_AdressId { get; set; }
        public long? VersandAdresse_AdressId { get; set; }

        public Beleg() : base()
        {
            BelegGuid = Guid.NewGuid();
        }
    }
}