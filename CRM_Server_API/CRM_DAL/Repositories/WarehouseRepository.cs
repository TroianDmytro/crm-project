using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AzureDbContext _context;

        public WarehouseRepository(AzureDbContext context) => _context = context;

        /// <summary>
        /// возвращает перечень складов
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            var result = await _context.Warehouses.ToListAsync();
            return result;
        }

        /// <summary>
        /// возвращает склады с перечнем товара на них
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Warehouse>> GetAllWithProductAsync()
        {
            List<Warehouse> warehouses = await _context.Warehouses
                .Include(wp => wp.WarehouseProducts)
                .ThenInclude(p => p.Products)
                .ThenInclude(c=>c.Categorys)
                .ToListAsync();

            // Проходим по каждому
            foreach (var warehouse in warehouses)
            {
                foreach (var prod in warehouse.WarehouseProducts)
                {
                    var product = prod.Products;

                    // Если продукт найден, присваиваем его количество
                    if (product != null)
                    {
                        product.Quantity = prod.QuantityStock;
                    }
                }
            }
            return warehouses;
        }

        /// <summary>
        /// возвращает склад по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Warehouse?> GetByIdAsync(Guid id)
        {
            var result = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        /// <summary>
        /// возвращает склад по id с перечнем товара на них
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Warehouse?> GetByIdWithProductsAsync(Guid id)
        {
            Warehouse? warehouses = await _context.Warehouses
                .Include(wp => wp.WarehouseProducts)
                .ThenInclude(p => p.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            return warehouses;
        }

        /// <summary>
        /// Добавить новий продукт на склад
        /// </summary>
        /// <param name="warehouseProduct">Обьект WarehouseProduct</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task AddProductToWarehouse(WarehouseProduct warehouseProduct)
        {
            bool recordExists = await _context.WarehouseProducts
                .AnyAsync(wp => wp.WarehouseId == warehouseProduct.WarehouseId && wp.ProductId == warehouseProduct.ProductId);

            if (recordExists)
                throw new Exception("На єтом складе уже есть такой товар.");

            if (warehouseProduct.QuantityStock < 0)
                throw new ArgumentOutOfRangeException("Количество товара не можит быть менше 0.");

            await _context.WarehouseProducts.AddAsync(warehouseProduct);
        }
        
        /// <summary>
        /// Создать новий склад
        /// </summary>
        /// <param name="item">Обьект Warehouse</param>
        /// <returns></returns>
        public async Task CreateAsync(Warehouse item)
        {
            await _context.Warehouses.AddAsync(item);
        }

        /// <summary>
        /// Редактивовать информацию склад
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Update(Warehouse item)
        {
            _context.Warehouses.Update(item);
        }

        /// <summary>
        /// Редактировать количество продукта на складе. Операция минус ( - ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое отнимется</param>
        /// <returns></returns>
        public async Task UpdateProductQuantitySubtracting(Guid warehouseid, Guid productId, int quantity)
        {
            WarehouseProduct? warehouse = await _context.WarehouseProducts.FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseid && wp.ProductId == productId);

            if (warehouse == null)
                return;

            if (warehouse.QuantityStock >= quantity)
                warehouse.QuantityStock -= quantity;
            else
            {
                throw new InvalidOperationException("Не доступная операция. Количество товара меньше необходимого количества.");
            }

            _context.WarehouseProducts.Update(warehouse);
        }

        /// <summary>
        /// Редактировать количество продукта на складе. Операция плюс ( + ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое добавится</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task UpdateProductQuantityAdd(Guid warehouseid, Guid productId, int quantity)
        {
            WarehouseProduct? warehouse = await _context.WarehouseProducts.FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseid && wp.ProductId==productId);

            if (warehouse == null)
                return;

            warehouse.QuantityStock += quantity;

            _context.WarehouseProducts.Update(warehouse);
        }

        /// <summary>
        /// Поиск псклада по условию
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Warehouse>> Find(Func<Warehouse, bool> predicate)
        {
            List<Warehouse> result = _context.Warehouses.Where(predicate).ToList();
            return result;
        }
        /// <summary>
        /// Проверяет существует такой склад или нет
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вернет true если существует</returns>
        public async Task<bool> IsExists(Guid id)
        {
            var result = await _context.Warehouses.AnyAsync(p => p.Id == id);
            return result;
        }

        /// <summary>
        /// Удаляет склад
        /// </summary>
        /// <param name="id">Id склада.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _context.Warehouses.Where(d => d.Id == id).ExecuteDeleteAsync();
        }

        /// <summary>
        /// Удаляет продук со склада
        /// </summary>
        /// <param name="warehouseId">ID склада</param>
        /// <param name="productId">ID продукта</param>
        /// <returns></returns>
        public async Task DeleteProductWithWarehouseAsync(Guid warehouseId, Guid productId)
        {
            await _context.WarehouseProducts.Where(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId).ExecuteDeleteAsync();
        }

       
    }
}
