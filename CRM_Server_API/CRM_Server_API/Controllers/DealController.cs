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

        /// <summary>
        /// Получаем список всех сделок
        /// </summary>
        /// <returns>Возвращает код 200 при успешном получении списка сделок</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet]
        public async Task<IActionResult> GetDealList()
        {
            var dealsList = await _dealService.GetAllDealsAsync();
            return Ok(dealsList);
        }

        /// <summary>
        /// Получаем сделку по ID
        /// </summary>
        /// <param name="id">Идентификатор сделки.</param>
        /// <returns> Возвращает сделку в формате <see cref="DealDTO"/></returns>
        /// <returns>Возвращает код 200 если есть сделка с указанным ID</returns>
        /// <returns>Возвращает код 404 если сделка с таким ID не найдена</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealId(Guid id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound("Deal with this Id not found");

            return Ok(deal);
        }

        /// <summary>
        /// Создаем сделку
        /// </summary>
        /// <param name="dealRequest">Данные для создания сделки в формате <see cref="DealRequest"/>.</param>
        /// <returns>Возвращает сделку в формате <see cref="DealDTO"/></returns>
        /// <returns>Возвращает код 200 при успешном создании сделки</returns>
        /// <returns>Возвращает код 404 если клиент с указанным ID не найден</returns>
        [HttpPost("create/")]
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

        /// <summary>
        /// Добавляем продукт в сделку
        /// </summary>
        /// <param name="dealProductDTO">Данные о продукте для добавления в сделку в формате <see cref="DealProductDTO"/>.</param>
        /// <returns>Возвращает код 200 и сообщение при успешном добавлении продукта в сделку</returns>
        /// <returns>Возвращает код 404 если продукт или сделка с указанным ID не найдены</returns>
        [HttpPost("add_product_to_deal/")]
        public async Task<IActionResult> AddProductToDeal([FromForm] DealProductDTO dealProductDTO)
        {
            try
            {
                await _dealProductService.AddProductToDeal(dealProductDTO);
                return Ok("Products added to deal successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //////////////////////////////expetion ebout name is null

        /// <summary>
        /// Обновляем данные сделки по ID
        /// </summary>
        /// <param name="id">Идентификатор сделки</param>
        /// <param name="dealUpdate">Обновленные данные сделки в формате</param>
        /// <returns>Возвращает код 200 при успешном изменении сделки. Возвращает код 404 если сделка с указанным ID не найдена</returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateDeal(Guid id, [FromForm] DealUpdate dealUpdate)
        {
            bool deal = await _dealService.DealIsExists(id);
            if (!deal)
                return NotFound($"Deal with id {id} not found");

            await _dealService.UpdateDealAsync(id,dealUpdate);

            return Ok("Deal updated successfully");
        }

        /// <summary>
        /// Удаляем сделку по ID
        /// </summary>
        /// <returns>Возвращает код 200 при успешном удалении сделки</returns>
        /// <returns>Возвращает код 404 если сделка с указанным ID не найдена</returns>
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteDeal(Guid id)
        {
            var deal = await _dealService.DealIsExists(id);
            if (!deal)
                return NotFound("Deal with this Id not found");

            await _dealService.DeleteDealAsync(id);
            return Ok();
        }
    }
}
