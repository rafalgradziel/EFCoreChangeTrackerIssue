namespace ContextRepro.DTOs
{
    public class BelegDTO
    {
        public Guid BelegGuid { get; set; }
        public BelegAdresseDTO BelegAdresse { get; set; }
        public BelegAdresseDTO VersandAdresse { get; set; }
        public BelegDTO()
        {
        }
    }
}
