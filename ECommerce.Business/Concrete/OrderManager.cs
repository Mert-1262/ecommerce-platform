using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Business.Payment;
using ECommerce.Business.Cargo;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        private readonly ICartDal _cartDal;

        private readonly IProductDal _productDal;

        private readonly PaymentAdapter _paymentAdapter;
        private readonly CargoFactory _cargoFactory;
        public OrderManager(IOrderDal orderDal, ICartDal cartDal, IProductDal productDal, PaymentAdapter paymentAdapter, CargoFactory cargoFactory)
        {
            _orderDal = orderDal;

            _cartDal = cartDal;

            _productDal = productDal;
            _paymentAdapter = paymentAdapter;
            _cargoFactory = cargoFactory;

        }

        public Order Checkout(int userId, CheckoutDto? checkout = null)
        {
            Cart cart = _cartDal.GetByUserId(userId);

            if (cart.CartItems == null || cart.CartItems.Count == 0)
            {
                throw new Exception("Sepet boş.");
            }

            decimal total = 0;

            Order order = new Order
            {
                UserId = userId,
                OrderStatus = "Pending",
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (CartItem item in cart.CartItems)
            {
                Product product = _productDal.GetById(item.ProductId);

                if (item.Quantity > product.Stock)
                {
                    throw new Exception(product.Name + " için stok yok.");
                }

                total += product.Price * item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                product.Stock -= item.Quantity;

                _productDal.Update(product);
            }

            order.TotalPrice = total;

            bool paymentResult =
                _paymentAdapter.MakePayment(
                    order.TotalPrice,
                    checkout?.PaymentMethod);

            if (!paymentResult)
            {
                throw new Exception("Ödeme başarısız.");
            }

            _orderDal.Add(order);
            ICargoService cargoService =
                _cargoFactory.CreateCargoService(checkout?.CargoCompany);

            CargoTrack cargoTrack =
                cargoService.CreateCargo(order.Id);

            _orderDal.AddCargoTrack(cargoTrack);

            order.CargoTrack = cargoTrack;
            _cartDal.ClearCartItems(userId);

            return order;
        }

        public List<Order> GetMyOrders(int userId)
        {
            return _orderDal.GetAllByUserId(userId);
        }

        public Order GetById(int orderId, int userId)
        {
            Order? order = _orderDal.GetById(orderId, userId);

            if (order == null)
            {
                throw new Exception("Sipariş bulunamadı.");
            }

            return order;
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetAll();
        }

        public void UpdateOrderStatus(int orderId, UpdateOrderStatusDto dto)
        {
            string[] allowedStatuses =
            {
                "Hazırlanıyor",
                "Kargoda",
                "Teslim Edildi"
            };

            if (!allowedStatuses.Contains(dto.OrderStatus))
            {
                throw new Exception("Geçersiz sipariş durumu.");
            }

            Order? order = _orderDal.GetByIdAdmin(orderId);

            if (order == null)
            {
                throw new Exception("Sipariş bulunamadı.");
            }

            order.OrderStatus = dto.OrderStatus;

            _orderDal.UpdateOrder(order);

            if (order.CargoTrack != null)
            {
                order.CargoTrack.Status = dto.OrderStatus;

                _orderDal.UpdateCargoTrack(order.CargoTrack);
            }
        }
    }
}
