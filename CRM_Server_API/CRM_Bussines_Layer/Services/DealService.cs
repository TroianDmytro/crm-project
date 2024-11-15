using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_Business_Layer.Services
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepository;
        private readonly AzureDbContext _dbContext;

        public DealService(IDealRepository dealRepository, AzureDbContext dbContext)
        {
            _dealRepository = dealRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Deal>> GetAllDealsAsync() => await _dealRepository.GetAllDealsAsync();

        public async Task<Deal> GetDealByIdAsync(int id) => await _dealRepository.GetDealByIdAsync(id);

        public async Task AddDealAsync(Deal deal) => await _dealRepository.AddDealAsync(deal);

        public async Task UpdateDealAsync(Deal deal) => await _dealRepository.UpdateDealAsync(deal);

        public async Task DeleteDealAsync(int id) => await _dealRepository.DeleteDealAsync(id);

        public async Task<decimal> GetProductPriceAsync(int productId)
        {
            return await _dealRepository.GetProductPriceAsync(productId);
        }

        public async Task AddProductToDealAsync(int dealId, int productId, int quantity)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found");
            }

            var dealProduct = new DealProduct
            {
                DealId = dealId,
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price
            };

            _dbContext.DealProducts.Add(dealProduct);
            await _dbContext.SaveChangesAsync();
        }
    }
}
