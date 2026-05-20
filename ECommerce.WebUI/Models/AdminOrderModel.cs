namespace ECommerce.WebUI.Models
{
    public class AdminOrderModel
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public UserSummaryModel? User { get; set; }

        public CargoTrackModel? CargoTrack { get; set; }
    }

    public class UserSummaryModel
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
