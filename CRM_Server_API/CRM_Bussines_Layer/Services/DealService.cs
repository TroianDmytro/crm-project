using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Infrastructure;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class DealService : IDealService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public DealService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<DealDTO>> GetAllDealsAsync()
        {
            var allDeal = await _context.Deal.GetAll();
            var result = _mapper.Map<List<DealDTO>>(allDeal);
            return result;
        }

        public async Task<DealDTO> GetDealByIdAsync(Guid id)
        {
            var deal = await _context.Deal.Get(id);
            var result = _mapper.Map<DealDTO>(deal);
            return result;
        }

        public async Task AddDealAsync(DealDTO dealDTO)
        {
            var deal = _mapper.Map<Deal>(dealDTO);

            deal.CreatedAt = await TimeUA.CurrentTimeAsync();
            deal.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _context.Deal.Create(deal);
            await _context.CommitChangesAsync();
        }

        public async Task UpdateDealAsync(DealDTO dealDTO)
        {
            var deal = _mapper.Map<Deal>(dealDTO);
            deal.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _context.Deal.Update(deal);
            await _context.CommitChangesAsync();
        }

        public async Task DeleteDealAsync(Guid id)
        {
            await _context.Deal.Delete(id);
            await _context.CommitChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public Task<decimal> GetProductPriceAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        //public Task AddProductToDealAsync(Guid dealId, Guid productId, int quantityTransaction)
        //{
        //    throw new NotImplementedException();
        //}
        
    }
}
