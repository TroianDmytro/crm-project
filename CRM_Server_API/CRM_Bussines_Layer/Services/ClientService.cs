using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Infrastructure;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_Business_Layer.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AzureDbContext _context;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, AzureDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ClientDTO> GetClientById(Guid id)
        {
            var client = await _unitOfWork.Client.Get(id);
            var clientDTO = _mapper.Map<ClientDTO>(client);
            return clientDTO;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClient()
        {
            var clients = await _unitOfWork.Client.GetAll();
            var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
            return clientsDTO;
        }

        public async Task<ClientDTO> CreateClient(ClientDTO newClient)
        {
            var client = _mapper.Map<Client>(newClient);

            client.CreatedAt = await TimeUA.CurrentTimeAsync();
            client.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Client.Create(client);
            await _unitOfWork.CommitChangesAsync();

            return newClient;
        }

        public async Task<ClientDTO> UpdateClient(ClientDTO updatedClient)
        {
            var client = _mapper.Map<Client>(updatedClient);

            client.UpdatedAt = await TimeUA.CurrentTimeAsync();

            await _unitOfWork.Client.Update(client);
            await _unitOfWork.CommitChangesAsync();

            return updatedClient;
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            var client = await _context.Clients
                                        .FirstOrDefaultAsync(c => c.Id == id);

            return client;  
        }

        public async Task DeleteClient(Guid id)
        {
            await _unitOfWork.Client.Delete(id);
            await _unitOfWork.CommitChangesAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
