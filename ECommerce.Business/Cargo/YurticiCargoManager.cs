using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Cargo
{
    public class YurticiCargoManager : ICargoService
    {
        public CargoTrack CreateCargo(int orderId)
        {
            return new CargoTrack
            {
                OrderId = orderId,
                CargoCompany = "Yurtiçi Kargo",
                TrackingNumber = Guid.NewGuid()
                    .ToString()
                    .Substring(0, 8),

                Status = "Hazırlanıyor"
            };
        }
    }
}