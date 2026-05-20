namespace ECommerce.WebUI.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal DiscountRate { get; set; }

        public bool IsActive { get; set; }
    }
}
