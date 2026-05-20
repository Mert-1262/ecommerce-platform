using ECommerce.Core.Utilities;

namespace ECommerce.Entities.Concrete
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public decimal DiscountRate { get; set; }

        public bool IsActive { get; set; }
    }
}
