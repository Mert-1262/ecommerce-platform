using ECommerce.Entities.Concrete;

namespace ECommerce.DataAccess.Abstract
{
    public interface ICampaignDal
    {
        Task<List<Campaign>> GetAllAsync();

        Task<Campaign?> GetByIdAsync(int id);

        Task<Campaign?> GetActiveAsync();

        Task AddAsync(Campaign campaign);

        Task<bool> UpdateStatusAsync(int id, bool isActive);

        Task DeleteAsync(Campaign campaign);
    }
}
