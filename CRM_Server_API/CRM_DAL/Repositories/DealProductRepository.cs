using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class DealProductRepository: IRepositoryDealProduct
    {
        private readonly AzureDbContext _context;

        public DealProductRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task Add(DealProduct item)
        {
            await _context.DealProducts.AddAsync(item);
        }

        public async Task Update(DealProduct item)
        {
            await Task.Run(()=>_context.DealProducts.Update(item));
        }

        public async Task Delete(DealProduct item)
        {
            await _context.DealProducts
                .Where(dp => dp.DealId == item.DealId && dp.ProductId == item.ProductId)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> DealExists(Guid item)
        {
            bool result = await _context.Deals.AnyAsync(d => d.DealId == item);
            return result;
        }

        public async Task<bool> ProductExists(Guid item)
        {
            bool result = await _context.Products.AnyAsync(d => d.ProductId == item);
            return result;
        }
    }
}
