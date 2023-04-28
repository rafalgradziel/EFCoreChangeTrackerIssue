
namespace ContextRepro.Entity.Belege
{
    public class Vorgang
    {
        public long VorgangId { get; set; }
        public Guid VorgangGuid { get; set; }
        public virtual List<Beleg> Belege { get; set; } = new List<Beleg>();
        public long KontaktId { get; set; }
        public virtual Kontakt Kontakt { get; set; }

        public Vorgang()
        {
            VorgangGuid = Guid.NewGuid();
        }
    }
}
