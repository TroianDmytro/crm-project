using CRM_Business_Layer.Services;
using CRM_DAL.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [HttpGet("AllDealsList")]
        public async Task<IActionResult> GetDealList()
        {
            var dealsList = await _dealService.GetAllDealsAsync();
            return Ok(dealsList);
        }

        [HttpGet("GetDealId")]
        public async Task<IActionResult> GetDealId(int id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            return Ok(deal);
        }

        [HttpPost("AddDeal")]
        public async Task<IActionResult> AddDeal([FromForm] string title, [FromForm] decimal amount, [FromForm] string status, [FromForm] int customerId)
        {
            var deal = new Deal
            {
                Title = title,
                Amount = amount,
                Status = status,
                CreatedAt = DateTime.UtcNow,
                CustomerId = customerId
            };

            await _dealService.AddDealAsync(deal);
            return CreatedAtAction(nameof(GetDealId), new { id = deal.DealId }, deal);
        }

        [HttpPost("AddProductToDeal")]
        public async Task<IActionResult> AddProductToDeal([FromForm] int dealId, [FromForm] int productId, [FromForm] int quantity)
        {
            try
            {
                await _dealService.AddProductToDealAsync(dealId, productId, quantity);
                return Ok("Product added to deal successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpPut("UpdateDealId")]
        public async Task<IActionResult> UpdateDeal(int id, [FromForm] string title, [FromForm] decimal amount, [FromForm] string status, [FromForm] int customerId)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            deal.Title = title;
            deal.Amount = amount;
            deal.Status = status;
            deal.CustomerId = customerId;

            await _dealService.UpdateDealAsync(deal);
            return NoContent();
        }

        [HttpDelete("DeleteDeal")]
        public async Task<IActionResult> DeleteDeal(int id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            await _dealService.DeleteDealAsync(id);
            return NoContent();
        }
    }
}
