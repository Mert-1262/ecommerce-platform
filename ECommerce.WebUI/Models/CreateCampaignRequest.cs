namespace ECommerce.WebUI.Models
{
    public class CreateCampaignRequest
    {
        public string Name { get; set; } = string.Empty;

        public decimal DiscountRate { get; set; }

        public bool IsActive { get; set; }
    }
}
