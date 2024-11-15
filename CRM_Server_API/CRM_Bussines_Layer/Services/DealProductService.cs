using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class DealProductService : IDealProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddProductToDeal(DealProductDTO dealProductDTO)
        {   
            DealProduct dealProduct = await DealAndProductExistsAsync(dealProductDTO);

            await _unitOfWork.DealProduct.Add(dealProduct);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task DeleteProductFromDeal(DealProductDTO dealProductDTO)
        {
            DealProduct dealProduct = await DealAndProductExistsAsync(dealProductDTO);

            await _unitOfWork.DealProduct.Delete(dealProduct);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task UpdateQuantityTransaction(DealProductDTO dealProductDTO)
        {
            DealProduct dealProduct = await DealAndProductExistsAsync(dealProductDTO);

            await _unitOfWork.DealProduct.Update(dealProduct);
            await _unitOfWork.CommitChangesAsync();
            
        }

        //checks if both objects exist. If so, it returns the DealProduct object.
        //If not, throws an error ArgumentNullException
        public async Task<DealProduct> DealAndProductExistsAsync(DealProductDTO dealProductDTO)
        {
            bool dealExists = await _unitOfWork.DealProduct.DealExists(dealProductDTO.DealId);
            if (!dealExists)
                throw new ArgumentNullException("Deal id not exists.");

            bool productExist = await _unitOfWork.DealProduct.ProductExists(dealProductDTO.ProductId);
            if (!productExist)
                throw new ArgumentNullException("Product is not exists.");

            return _mapper.Map<DealProduct>(dealProductDTO);
        }
    }
}
