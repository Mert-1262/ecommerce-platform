namespace ECommerce.Business.Payment
{
    public class CreditCardPaymentManager : IPaymentService
    {
        public bool Pay(decimal amount)
        {
            return true;
        }
    }
}