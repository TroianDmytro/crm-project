using CRM_DAL.Entitys;

namespace CRM_Business_Layer.Services
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<(bool IsSuccess, string? ErrorMessage, Customer? Customer)> AddCustomerAsync(string name, string phone, string email, string address, string status);
        Task<(bool IsSuccess, string? ErrorMessage)> UpdateCustomerAsync(int id, string name, string phone, string email, string address, string status);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
