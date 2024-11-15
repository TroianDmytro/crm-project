using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("deal/")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IDealProductService _dealProductService;
        public DealController(IDealService dealService, IDealProductService dealProductService)
        {
            _dealService = dealService;
            _dealProductService = dealProductService;
        }

        [HttpGet("AllDealsList")]
        public async Task<IActionResult> GetDealList()
        {
            var dealsList = await _dealService.GetAllDealsAsync();
            return Ok(dealsList);
        }

        [HttpGet("GetDealId")]
        public async Task<IActionResult> GetDealId(Guid id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            return Ok(deal);
        }

        [HttpPost("AddDeal")]
        public async Task<IActionResult> AddDeal([FromForm] string title, [FromForm] decimal amount, [FromForm] string status)
        {
            var deal = new DealDTO
            {
                Title = title,
                Amount = amount,
                Status = status,
                CreatedAt = DateTime.UtcNow,
            };

            await _dealService.AddDealAsync(deal);
            return CreatedAtAction(nameof(GetDealId), new { id = deal.DealId }, deal);
        }

        [HttpPost("AddProductToDeal")]
        public async Task<IActionResult> AddProductToDeal([FromForm] DealProductDTO dealProductDTO)
        {
            try
            {
                await _dealProductService.AddProductToDeal(dealProductDTO);
                return Ok("Product added to deal successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateDealId")]
        public async Task<IActionResult> UpdateDeal(Guid id, [FromForm] string title, [FromForm] decimal amount, [FromForm] string status, [FromForm] int customerId)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            deal.Title = title;
            deal.Amount = amount;
            deal.Status = status;

            await _dealService.UpdateDealAsync(deal);
            return NoContent();
        }

        [HttpDelete("DeleteDeal")]
        public async Task<IActionResult> DeleteDeal(Guid id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            await _dealService.DeleteDealAsync(id);
            return NoContent();
        }
    }
}
