using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Infrastructure;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class WarehousesService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WarehousesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WarehouseDTO>> GetAllWarehousesAsync()
        {
            var result = await _unitOfWork.Warehouse.GetAllAsync();
            List<WarehouseDTO> resultDTO = _mapper.Map<List<WarehouseDTO>>(result);
            return resultDTO;
        }

        public async Task<WarehouseDTO> GetWarehousesByIdAsync(Guid id)
        {
            var result = await _unitOfWork.Warehouse.GetAsync(id);
            WarehouseDTO resultDTO = _mapper.Map<WarehouseDTO>(result);
            return resultDTO;
        }

        public async Task AddWarehousesAsync(WarehouseDTO warehouseDTO)
        {
            Warehouse warehouse = _mapper.Map<Warehouse>(warehouseDTO);

            warehouse.CreatedAt = await TimeUA.CurrentTimeAsync();
            warehouse.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Warehouse.CreateAsync(warehouse);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task UpdateWarehousesAsync(Guid id, WarehouseDTO update)
        {
            Warehouse? warehouse = await _unitOfWork.Warehouse.GetAsync(id) ??
                                   throw new ArgumentException("Warehouse id not found. UpdateWarehousesAsync method. "); ;

            warehouse.Name = update.Name;
            warehouse.Location = update.Location;
            warehouse.Description = update.Description;
            warehouse.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Warehouse.Update(warehouse);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> IsExists(Guid warehouseId)
        {
            bool result = await _unitOfWork.Warehouse.IsExists(warehouseId);
            return result;
        }

        public async Task DeleteWarehousesAsync(Guid id)
        {
            await _unitOfWork.Warehouse.DeleteAsync(id);
            await _unitOfWork.CommitChangesAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
