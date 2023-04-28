using System.Collections.ObjectModel;

namespace ContextRepro.DTOs
{
    public class VorgangDTO
    {
        public Guid VorgangGuid { get; set; }
        public virtual IList<BelegDTO> Belege { get; set; }
        public Guid KundeGuid { get; set; }

        public VorgangDTO()
        {
            Belege = new ObservableCollection<BelegDTO>();
        }
    }
}
