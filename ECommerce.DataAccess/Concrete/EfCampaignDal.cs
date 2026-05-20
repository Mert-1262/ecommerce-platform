using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Contexts;
using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete
{
    public class EfCampaignDal : ICampaignDal
    {
        private readonly ECommerceDbContext _context;

        public EfCampaignDal(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Campaign>> GetAllAsync()
        {
            return await _context.Campaigns
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<Campaign?> GetByIdAsync(int id)
        {
            return await _context.Campaigns
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campaign?> GetActiveAsync()
        {
            return await _context.Campaigns
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Campaign campaign)
        {
            if (campaign.IsActive)
            {
                List<Campaign> activeCampaigns = await _context.Campaigns
                    .Where(x => x.IsActive)
                    .ToListAsync();

                foreach (Campaign activeCampaign in activeCampaigns)
                {
                    activeCampaign.IsActive = false;
                }
            }

            await _context.Campaigns.AddAsync(campaign);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatusAsync(int id, bool isActive)
        {
            bool campaignExists = await _context.Campaigns
                .AnyAsync(x => x.Id == id);

            if (!campaignExists)
            {
                return false;
            }

            if (isActive)
            {
                await _context.Campaigns
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(
                            campaign => campaign.IsActive,
                            campaign => campaign.Id == id));

                return true;
            }

            await _context.Campaigns
                .Where(campaign => campaign.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(campaign => campaign.IsActive, false));

            return true;
        }

        public async Task DeleteAsync(Campaign campaign)
        {
            _context.Campaigns.Remove(campaign);

            await _context.SaveChangesAsync();
        }
    }
}
