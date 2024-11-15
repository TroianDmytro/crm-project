using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly AzureDbContext _context;

        public CustomerRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                                 .Include(c => c.Deals)
                                 .ThenInclude(d => d.DealProducts)
                                 .ThenInclude(dp => dp.Product)
                                 .FirstOrDefaultAsync(c => c.CustomerId == id);
        }


        public async Task<Customer?> GetByEmailOrPhoneAsync(string email, string phone)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email || c.Phone == phone);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
