using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
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
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        // GET: client/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ClientDTO> result = await _clientService.GetAllClient();
            return Ok(result.ToList());
        }

        // GET client/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ClientDTO result = await _clientService.GetClientById(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

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

        // PUT client/edit/5
        [HttpPut("edit/{id}")] // настроить дату обновления
        public async Task<IActionResult> Put(Guid id, [FromBody] ClientRequest clientRequest)
        {
            ClientDTO newClientDTO = _mapper.Map<ClientDTO>(clientRequest);
            ClientDTO clientDTO = await _clientService.UpdateClient(newClientDTO);

            if (clientDTO == null)
                return BadRequest();

            return Ok(clientDTO);
        }

        // DELETE  client/remove/5
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientService.DeleteClient(id);
            return Ok();
        }
    }
}
