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

        /// <summary>
        /// возвращает перечень складов
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WarehouseDTO>> GetAllWarehousesAsync()
        {
            var result = await _unitOfWork.Warehouse.GetAllAsync();
            List<WarehouseDTO> resultDTO = _mapper.Map<List<WarehouseDTO>>(result);
            return resultDTO;
        }
        /// <summary>
        /// возвращает склады с перечнем товара на них
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WarehouseDTO>> GetAllWarehouseWithProductAsync()
        {
            var result = await _unitOfWork.Warehouse.GetAllWithProductAsync();
            List<WarehouseDTO> resultDTO = _mapper.Map<List<WarehouseDTO>>(result);

            return resultDTO;
        }

        /// <summary>
        /// возвращает склад по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WarehouseDTO> GetWarehouseByIdAsync(Guid id)
        {
            var result = await _unitOfWork.Warehouse.GetByIdAsync(id);
            WarehouseDTO resultDTO = _mapper.Map<WarehouseDTO>(result);
            return resultDTO;
        }

        /// <summary>
        /// возвращает склад по id с перечнем товара на них
        /// </summary>
        /// <param name="id">ID склада</param>
        /// <returns></returns>
        public async Task<WarehouseDTO?> GetWarehouseByIdWithProductsAsync(Guid id)
        {
            var result = await _unitOfWork.Warehouse.GetByIdWithProductsAsync(id);
            WarehouseDTO? resultDTO = _mapper.Map<WarehouseDTO?>(result);
            return resultDTO;
        }

        /// <summary>
        /// Создать новий склад
        /// </summary>
        /// <param name="warehouseDTO">Обьект Warehouse</param>
        /// <returns></returns>
        public async Task AddWarehousesAsync(WarehouseDTO warehouseDTO)
        {
            Warehouse warehouse = _mapper.Map<Warehouse>(warehouseDTO);

            warehouse.CreatedAt = await TimeUA.CurrentTimeAsync();
            warehouse.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Warehouse.CreateAsync(warehouse);
            await _unitOfWork.CommitChangesAsync();
        }


        /// <summary>
        /// Добавить новий продукт на склад
        /// </summary>
        /// <param name="warehouseProductDTO">Обьект WarehouseProduct</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task AddProductToWarehouse(WarehouseProductDTO warehouseProductDTO)
        {
            WarehouseProduct warehouseProduct = _mapper.Map<WarehouseProduct>(warehouseProductDTO);
            await _unitOfWork.Warehouse.AddProductToWarehouse(warehouseProduct);
            await _unitOfWork.CommitChangesAsync();
        }

        /// <summary>
        /// Редактивовать информацию склад
        /// </summary>
        /// <param name="id">id склада</param>
        /// <param name="update">Обьект WarehouseDTO</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task UpdateWarehousesAsync(Guid id, WarehouseDTO update)
        {
            Warehouse? warehouse = await _unitOfWork.Warehouse.GetByIdAsync(id) ??
                                   throw new ArgumentException("Warehouse id not found. UpdateWarehousesAsync method. "); ;

            warehouse.Name = update.Name;
            warehouse.Location = update.Location;
            warehouse.Description = update.Description;
            warehouse.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Warehouse.Update(warehouse);
            await _unitOfWork.CommitChangesAsync();
        }

        /// <summary>
        /// Редактировать количество продукта на складе. Операция минус ( - ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое отнимется</param>
        /// <returns></returns>
        public async Task UpdateProductQuantitySubtracting(Guid id, int quantity)
        {
            await _unitOfWork.Warehouse.UpdateProductQuantitySubtracting(id, quantity);
            await _unitOfWork.CommitChangesAsync();
        }

        /// <summary>
        /// Редактировать количество продукта на складе. Операция плюс ( + ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое добавится</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task UpdateProductQuantityAdd(Guid id, int quantity)
        {
            await UpdateProductQuantityAdd(id, quantity);
            await _unitOfWork.CommitChangesAsync();
        }

        /// <summary>
        /// Проверяет существует такой склад или нет
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns>Вернет true если существует</returns>
        public async Task<bool> IsExists(Guid warehouseId)
        {
            bool result = await _unitOfWork.Warehouse.IsExists(warehouseId);
            return result;
        }

        /// <summary>
        /// Удаляет склад
        /// </summary>
        /// <param name="id">Id склада.</param>
        /// <returns></returns>
        public async Task DeleteWarehousesAsync(Guid id)
        {
            await _unitOfWork.Warehouse.DeleteAsync(id);
            await _unitOfWork.CommitChangesAsync();
        }

        /// <summary>
        /// Удаляет продук со склада
        /// </summary>
        /// <param name="warehouseId">ID склада</param>
        /// <param name="productId">ID продукта</param>
        /// <returns></returns>
        public async Task DeleteProductWithWarehouseAsync(Guid warehouseId, Guid productId)
        {
            await _unitOfWork.Warehouse.DeleteProductWithWarehouseAsync(warehouseId, productId);
            await _unitOfWork.CommitChangesAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        
    }
}
