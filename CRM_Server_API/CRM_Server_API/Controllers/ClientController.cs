using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_Server_API.Blobs;
using CRM_Server_API.Models.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_Server_API.Controllers
{
    [Route("client/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly BlobModul _blobModul;
        public ClientController(IClientService clientService, IMapper mapper, BlobModul blobModul)
        {
            _clientService = clientService;
            _mapper = mapper;
            _blobModul = blobModul;
        }

        /// <summary>
        /// Получаем список всех клиентов
        /// </summary>
        /// <returns>Возвращаем код 200 при успшном получении списка клиентов</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        // GET: client/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ClientDTO> result = await _clientService.GetAllClient();

            foreach (ClientDTO client in result)
            {
                foreach (var deal in client.DealDTOs)
                {
                    foreach (var product in deal.ProductDTOs)
                    {
                        if (product.PhotoBlob != null)
                            product.PhotoBlob = await _blobModul.Download(product.PhotoBlob);
                    }
                }
            }
            return Ok(result.ToList());
        }

        /// <summary>
        /// Получаем клиента по ID
        /// </summary>
        /// <param name="id">Идентификатор клиента/></param>
        /// <returns>Возвращает данные клиента в формате <see cref="ClientDTO"/></returns>
        /// <returns>Возвращает код 200 при успешном получении  клиента</returns>
        /// <returns>Возвращает код 400 если клиент с указанным ID не найден</returns>
        // GET client/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ClientDTO result = await _clientService.GetClientById(id);
            if (result == null)
                return BadRequest();

            foreach (var deal in result.DealDTOs)
            {
                foreach (var product in deal.ProductDTOs)
                {
                    if (product.PhotoBlob != null)
                        product.PhotoBlob = await _blobModul.Download(product.PhotoBlob);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Создаем клиента
        /// </summary>
        /// <param name="newClient">Клиент в формате <see cref="ClientRequest"/></param>
        /// <returns>Возвращает созданного клиента в формате <see cref="ClientDTO"/></returns>
        /// <returns>Возвращает код 200 при успешном создании клиента</returns>
        /// <returns>Возвращает код 400 если не удалось создать клиента</returns>
        // POST client/add/
        [HttpPost("add/")]
        public async Task<IActionResult> Post([FromBody] ClientRequest newClient)
        {
            ClientDTO newClientDTO = _mapper.Map<ClientDTO>(newClient);
            ClientDTO clientDTO = await _clientService.CreateClient(newClientDTO);

            if (clientDTO == null)
                return BadRequest();

            return Ok(clientDTO);
        }

        /// <summary>
        /// Обновляем информацию клиента
        /// </summary>
        /// <param name="id">Идентификатор клиента/></param>
        /// <param name="clientRequest">Данные клиента в формате <see cref="ClientRequest"/></param>
        /// <returns>Возвращает обновленные данные клиента в формате <see cref="ClientDTO"/></returns>
        /// <returns>Возвращает код 200 при успешном обновлении клиента</returns>
        /// <returns>Возвращает код 404 если не удалось обновить клиента</returns>
        // PUT client/edit/5
        [HttpPut("edit/{id}")] // настроить дату обновления
        public async Task<IActionResult> Put(Guid id, [FromBody] ClientRequest clientRequest)
        {
            ClientDTO? clientDTO = await _clientService.GetClientById(id);

            if (clientDTO == null)
                return NotFound("Not found client.");

            clientDTO.Name = clientRequest.Name ?? clientDTO.Name;
            clientDTO.LastName = clientRequest.LastName ?? clientDTO.LastName;
            clientDTO.Email = clientRequest.Email ?? clientDTO.Email;
            clientDTO.PhoneNumber = clientRequest.PhoneNumber ?? clientDTO.PhoneNumber;
            clientDTO.Address = clientRequest.Address ?? clientDTO.Address;
            clientDTO.CompanyName = clientRequest.CompanyName ?? clientDTO.CompanyName;
            clientDTO.Notes = clientRequest.Notes ?? clientDTO.Notes;
            clientDTO.IsActive = (bool)(clientRequest.IsActive ?? clientRequest.IsActive);

            ClientDTO updateClientDTO = await _clientService.UpdateClient(clientDTO);

            return Ok(updateClientDTO);
        }

        /// <summary>
        /// Удалить клиента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор клиента/></param>
        /// <returns>Возвращает код 200 при успешном удалении клиента</returns>
        /// <returns>Возвращает код 400 если не удалось удалить клиента</returns>
        // DELETE  client/remove/5
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientService.DeleteClient(id);
            return Ok();
        }
    }
}
