using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Concrete
{
    public class CampaignManager : ICampaignService
    {
        private readonly ICampaignDal _campaignDal;

        public CampaignManager(ICampaignDal campaignDal)
        {
            _campaignDal = campaignDal;
        }

        public async Task<List<Campaign>> GetAllAsync()
        {
            return await _campaignDal.GetAllAsync();
        }

        public async Task<Campaign?> GetActiveAsync()
        {
            return await _campaignDal.GetActiveAsync();
        }

        public async Task AddAsync(CreateCampaignRequest request)
        {
            Validate(request.Name, request.DiscountRate);

            Campaign campaign = new()
            {
                Name = request.Name,
                DiscountRate = request.DiscountRate,
                IsActive = request.IsActive
            };

            await _campaignDal.AddAsync(campaign);
        }

        public async Task UpdateStatusAsync(int id, UpdateCampaignStatusRequest request)
        {
            bool updated = await _campaignDal.UpdateStatusAsync(id, request.IsActive);

            if (!updated)
            {
                throw new Exception("Kampanya bulunamadı.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            Campaign campaign = await GetCampaignAsync(id);

            await _campaignDal.DeleteAsync(campaign);
        }

        private async Task<Campaign> GetCampaignAsync(int id)
        {
            Campaign? campaign = await _campaignDal.GetByIdAsync(id);

            if (campaign == null)
            {
                throw new Exception("Kampanya bulunamadı.");
            }

            return campaign;
        }

        private static void Validate(string name, decimal discountRate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Kampanya adı zorunludur.");
            }

            if (discountRate <= 0 || discountRate > 100)
            {
                throw new Exception("İndirim oranı 1 ile 100 arasında olmalıdır.");
            }
        }
    }
}
