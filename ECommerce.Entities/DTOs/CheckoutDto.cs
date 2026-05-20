namespace ECommerce.Entities.DTOs
{
    public class CheckoutDto
    {
        public string PaymentMethod { get; set; } = "CreditCard";

        public string CargoCompany { get; set; } = "Aras";
    }
}
