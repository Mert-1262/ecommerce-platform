using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Cargo
{
    public interface ICargoService
    {
        CargoTrack CreateCargo(int orderId);
    }
}