using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface ICampaignService
    {
        Task<List<Campaign>> GetAllAsync();

        Task<Campaign?> GetActiveAsync();

        Task AddAsync(CreateCampaignRequest request);

        Task UpdateStatusAsync(int id, UpdateCampaignStatusRequest request);

        Task DeleteAsync(int id);
    }
}
