namespace ECommerce.Business.Payment
{
    public interface IPaymentService
    {
        bool Pay(decimal amount);
    }
}
