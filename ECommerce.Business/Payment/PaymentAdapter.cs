namespace ECommerce.Business.Payment
{
    public class PaymentAdapter
    {
        private readonly IPaymentService _paymentService;

        public PaymentAdapter(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public bool MakePayment(decimal amount, string? paymentMethod = null)
        {
            IPaymentService paymentService = ResolvePaymentService(paymentMethod);
            return paymentService.Pay(amount);
        }

        private IPaymentService ResolvePaymentService(string? paymentMethod)
        {
            return paymentMethod switch
            {
                "Havale" => new HavalePaymentManager(),
                "CreditCard" => new CreditCardPaymentManager(),
                _ => _paymentService
            };
        }
    }
}