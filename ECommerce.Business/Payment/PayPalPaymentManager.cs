namespace ECommerce.Business.Payment
{
    public class PayPalPaymentManager : IPaymentService
    {
        public bool Pay(decimal amount)
        {
            return true;
        }
    }
}