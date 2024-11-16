using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("deal/")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IDealProductService _dealProductService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public DealController(IMapper mapper, IDealService dealService, IDealProductService dealProductService, IClientService clientService)
        {
            _mapper = mapper;
            _dealService = dealService;
            _dealProductService = dealProductService;
            _clientService = clientService;
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
        public async Task<IActionResult> AddDeal([FromForm] DealRequest dealRequest)
        {
            var client = await _clientService.GetClientById(dealRequest.ClientId);

            if (client == null)
                return NotFound("Client with this Id not found.");

            DealDTO dealDTO = _mapper.Map<DealDTO>(dealRequest);

            dealDTO.DealId = Guid.NewGuid();

            await _dealService.AddDealAsync(dealDTO);

            return Ok(dealDTO);
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
        public async Task<IActionResult> UpdateDeal(Guid id, [FromBody] UpdateDealDTO updateDealDTO)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            deal.Title = updateDealDTO.Title;
            deal.Amount = updateDealDTO.Amount;
            deal.Status = updateDealDTO.Status;
            deal.ClientId = updateDealDTO.ClientId;

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
