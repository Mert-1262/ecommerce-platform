namespace ECommerce.WebUI.Models
{
    public class CheckoutRequest
    {
        public string PaymentMethod { get; set; } = "CreditCard";

        public string CargoCompany { get; set; } = "Aras";
    }
}
