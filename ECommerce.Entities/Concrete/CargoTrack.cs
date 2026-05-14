using ECommerce.Core.Utilities;

namespace ECommerce.Entities.Concrete
{
    public class CargoTrack : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string CargoCompany { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
    }
}
