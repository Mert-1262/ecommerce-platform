using ECommerce.Entities.Concrete;

namespace ECommerce.DataAccess.Abstract
{
    public interface IOrderDal
    {
        void Add(Order order);

        void AddCargoTrack(CargoTrack cargoTrack);

        Order? GetById(int orderId, int userId);

        List<Order> GetAllByUserId(int userId);

        List<Order> GetAll();

        Order? GetByIdAdmin(int orderId);

        void UpdateOrder(Order order);

        void UpdateCargoTrack(CargoTrack cargoTrack);
    }
}
