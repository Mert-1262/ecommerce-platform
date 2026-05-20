namespace ECommerce.Business.Cargo
{
    public class CargoFactory
    {
        public ICargoService CreateCargoService(string? cargoCompany = null)
        {
            if (!string.IsNullOrWhiteSpace(cargoCompany))
            {
                return cargoCompany switch
                {
                    "Aras" => new ArasCargoManager(),
                    "Yurtici" => new YurticiCargoManager(),
                    _ => throw new ArgumentException("Geçersiz kargo firması.")
                };
            }

            Random random = new();

            int value = random.Next(1, 3);

            if (value == 1)
            {
                return new ArasCargoManager();
            }

            return new YurticiCargoManager();
        }
    }
}