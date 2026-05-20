namespace ECommerce.WebUI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public List<OrderItemModel> OrderItems { get; set; } = new();

        public CargoTrackModel? CargoTrack { get; set; }
    }

    public class CargoTrackModel
    {
        public string CargoCompany { get; set; } = string.Empty;

        public string TrackingNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }

    public class OrderItemModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public ProductModel? Product { get; set; }
    }
}
