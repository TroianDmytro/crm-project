using CRM_Business_Layer.DTO;
using CRM_DAL.Entitys;

namespace CRM_Business_Layer.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetAllClient();
        Task<ClientDTO> GetClientById(Guid id);
        Task<ClientDTO> CreateClient(ClientDTO newClient);
        Task<ClientDTO> UpdateClient(ClientDTO updatedClient);
        Task DeleteClient(Guid id);
        void Dispose();
        Task<Client> GetClientByIdAsync(Guid clientId);
    }
}
