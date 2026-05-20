using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface IOrderService
    {
        Order Checkout(int userId, CheckoutDto? checkout = null);

        List<Order> GetMyOrders(int userId);

        Order GetById(int orderId, int userId);

        List<Order> GetAllOrders();

        void UpdateOrderStatus(int orderId, UpdateOrderStatusDto dto);
    }
}
