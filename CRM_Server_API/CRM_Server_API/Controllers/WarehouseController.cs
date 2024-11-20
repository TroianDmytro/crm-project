using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("warehouse/")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouse;
        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseService warehouse, IMapper mapper)
        {
            _warehouse = warehouse;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех складов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllWarehouse()
        {
            IEnumerable<WarehouseDTO> listWarehouses = await _warehouse.GetAllWarehousesAsync();
            List<WarehouseDTO> result = listWarehouses.ToList();
            return Ok(result);
        }

        /// <summary>
        ///  Получение склада по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(Guid id)
        {
            WarehouseDTO warehouse = await _warehouse.GetWarehouseByIdAsync(id);
            if (warehouse == null) 
                return NotFound();

            return Ok(warehouse);
        }

        /// <summary>
        /// Получение всех складов с товарами
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_all_with_products/")]
        public async Task<IActionResult> GetAllWarehouseWithProducts()
        {
            var warehouse = await _warehouse.GetAllWarehouseWithProductAsync();
            List<WarehouseDTO> warehouseDTO = warehouse.ToList();
            return Ok(warehouseDTO);
        }

        /// <summary>
        /// Получение по id с товарами
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_with_products/{id}")]
        public async Task<IActionResult> GetWarehouseByIdWithProduct(Guid id)
        {
            var result = await _warehouse.GetWarehouseByIdWithProductsAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Добавление склада
        /// </summary>
        /// <param name="warehouseRequest"></param>
        /// <returns></returns>
        [HttpPost("add_warehouse/")]
        public async Task<IActionResult> AddWarehouse([FromBody] WarehouseRequest warehouseRequest)
        {
            WarehouseDTO warehouse = _mapper.Map<WarehouseDTO>(warehouseRequest);
            await _warehouse.AddWarehousesAsync(warehouse);

            return Ok();
        }

        /// <summary>
        /// Добавление продукта в склад
        /// </summary>
        /// <param name="warehouseProductRequest"></param>
        /// <returns></returns>
        [HttpPost("add_product_to_warehouse/")]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] WarehouseProductRequest warehouseProductRequest)
        {
            WarehouseDTO warehouse = _mapper.Map<WarehouseDTO>(warehouseProductRequest);
            await _warehouse.AddWarehousesAsync(warehouse);

            return Ok();
        }

        /// <summary>
        /// Редактирование склада
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouseRequest"></param>
        /// <returns></returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateWarehouse(Guid id, [FromBody] WarehouseRequest warehouseRequest)
        {
            try
            {
                WarehouseDTO warehouseDTO = _mapper.Map<WarehouseDTO>(warehouseRequest);
                await _warehouse.UpdateWarehousesAsync(id, warehouseDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
            return Ok();
        }


        /// <summary>
        /// Редактировать количество продукта на складе. Операция минус ( - ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое отнимется</param>
        /// <returns></returns>
        [HttpPut("edit_product_sub/{id}")]
        public async Task<IActionResult> UpdateProductSubtracting(Guid id, int quantity)
        {
            await _warehouse.UpdateProductQuantitySubtracting(id, quantity);
            return Ok();
        }

        /// <summary>
        /// Редактировать количество продукта на складе. Операция плюс ( + ).
        /// </summary>
        /// <param name="id">id записи склад-продукт</param>
        /// <param name="quantity">Количество которое добавится</param>
        /// <returns></returns>
        [HttpPut("edit_product_add/{id}")]
        public async Task<IActionResult> UpdateProductQuantityAdd(Guid id, int quantity)
        {
            await _warehouse.UpdateProductQuantityAdd(id, quantity);
            return Ok();
        }

        /// <summary>
        /// Удаляет продук со склада
        /// </summary>
        /// <param name="warehouseId">ID склада</param>
        /// <param name="productId">ID продукта</param>
        /// <returns></returns>
        [HttpDelete("remove_product/")]
        public async Task<IActionResult> DeleteProductWithWarehouse(Guid warehouseId, Guid productId)
        {
            await _warehouse.DeleteProductWithWarehouseAsync(warehouseId, productId);
            return NoContent();
        }

        /// <summary>
        /// Удаляет склад
        /// </summary>
        /// <param name="id">Id склада.</param>
        /// <returns></returns>
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            bool warehauseIsExists = await _warehouse.IsExists(id);
            if (!warehauseIsExists)
                return NotFound("Warehouse with this Id not found");

            await _warehouse.DeleteWarehousesAsync(id);
            return NoContent();
        }




    }
}
