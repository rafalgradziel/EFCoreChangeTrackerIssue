namespace ContextRepro.DTOs
{
    public class BelegAdresseDTO
    {
        public Guid AdressGuid { get; set; }
        public string Name { get; set; }

        public BelegAdresseDTO()
        {
            AdressGuid = Guid.NewGuid();
        }
    }
}