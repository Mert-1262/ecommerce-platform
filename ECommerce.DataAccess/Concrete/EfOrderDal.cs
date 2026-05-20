using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Contexts;
using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete
{
    public class EfOrderDal : IOrderDal
    {
        private readonly ECommerceDbContext _context;

        public EfOrderDal(ECommerceDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);

            _context.SaveChanges();
        }

        public void AddCargoTrack(CargoTrack cargoTrack)
        {
            _context.CargoTracks.Add(cargoTrack);

            _context.SaveChanges();
        }

        public Order? GetById(int orderId, int userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .Include(o => o.CargoTrack)
                .FirstOrDefault(o => o.Id == orderId && o.UserId == userId);
        }

        public List<Order> GetAllByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .Include(o => o.CargoTrack)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .Include(o => o.CargoTrack)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetByIdAdmin(int orderId)
        {
            return _context.Orders
                .Include(o => o.CargoTrack)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);

            _context.SaveChanges();
        }

        public void UpdateCargoTrack(CargoTrack cargoTrack)
        {
            _context.CargoTracks.Update(cargoTrack);

            _context.SaveChanges();
        }
    }
}
