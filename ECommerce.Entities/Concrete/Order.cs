using ECommerce.Core.Utilities;

namespace ECommerce.Entities.Concrete
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public CargoTrack CargoTrack { get; set; }
    }
}
