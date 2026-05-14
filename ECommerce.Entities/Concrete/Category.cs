using ECommerce.Core.Utilities;

namespace ECommerce.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}