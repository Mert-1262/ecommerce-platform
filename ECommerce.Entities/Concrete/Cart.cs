using ECommerce.Core.Utilities;

namespace ECommerce.Entities.Concrete
{
     public class Cart : BaseEntity
    
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
