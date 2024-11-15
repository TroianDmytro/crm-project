using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, Customer? Customer)> AddCustomerAsync(
            string name, string phone, string email, string address, string status)
        {
            var existingCustomer = await _customerRepository.GetByEmailOrPhoneAsync(email, phone);
            if (existingCustomer != null)
            {
                return (false, "Customer with this email or phone already exists.", null);
            }

            var customer = new Customer
            {
                Name = name,
                Phone = phone,
                Email = email,
                Address = address,
                Status = status,
                CreatedAt = DateTime.UtcNow
            };

            await _customerRepository.AddCustomerAsync(customer);
            return (true, null, customer);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateCustomerAsync(
            int id, string name, string phone, string email, string address, string status)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return (false, "NotFound");
            }

            customer.Name = name ?? customer.Name;
            customer.Phone = phone ?? customer.Phone;
            customer.Email = email ?? customer.Email;
            customer.Address = address ?? customer.Address;
            customer.Status = status ?? customer.Status;

            await _customerRepository.UpdateCustomerAsync(customer);
            return (true, null);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return false;
            }

            await _customerRepository.DeleteCustomerAsync(customer);
            return true;
        }
    }
}
