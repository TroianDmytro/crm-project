using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Business_Layer.Services;
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

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouse()
        {
            IEnumerable<WarehouseDTO> listWarehouses = await _warehouse.GetAllWarehousesAsync();
            List<WarehouseDTO> result = listWarehouses.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllWarehouse(Guid id)
        {
            WarehouseDTO warehouse = await _warehouse.GetWarehousesByIdAsync(id);
            if (warehouse == null) 
                return NotFound();

            return Ok(warehouse);
        }

        [HttpPost("add/")]
        public async Task<IActionResult> AddWarehouse([FromBody] WarehouseRequest warehouseRequest)
        {
            WarehouseDTO warehouse = _mapper.Map<WarehouseDTO>(warehouseRequest);
            await _warehouse.AddWarehousesAsync(warehouse);

            return Ok();
        }

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


        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            bool warehauseIsExists = await _warehouse.IsExists(id);
            if (!warehauseIsExists)
                return NotFound("Warehouse with this Id not found");

            await _warehouse.DeleteWarehousesAsync(id);
            return Ok();
        }




    }
}
