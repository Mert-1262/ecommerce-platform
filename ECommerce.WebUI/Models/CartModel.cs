namespace ECommerce.WebUI.Models
{
    public class CartModel
    {
        public int Id { get; set; }

        public List<CartItemModel> CartItems { get; set; } = new();
    }

    public class CartItemModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public ProductModel? Product { get; set; }
    }
}
