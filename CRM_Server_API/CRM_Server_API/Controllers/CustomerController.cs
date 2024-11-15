using CRM_Business_Layer.Services;
using CRM_DAL.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomerId")]
        public async Task<ActionResult<Customer>> GetCustomerId(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound("Customer with this Id not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(
            [FromForm] string name,
            [FromForm] string phone,
            [FromForm] string email,
            [FromForm] string address,
            [FromForm] string status)
        {
            var result = await _customerService.AddCustomerAsync(name, phone, email, address, status);

            if (!result.IsSuccess)
            {
                return Conflict(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetCustomerId), new { id = result.Customer.CustomerId }, result.Customer);
        }

        [HttpPut("UpdateCustomerId")]
        public async Task<IActionResult> UpdateCustomer(int id,
            [FromForm] string name,
            [FromForm] string phone,
            [FromForm] string email,
            [FromForm] string address,
            [FromForm] string status)
        {
            var result = await _customerService.UpdateCustomerAsync(id, name, phone, email, address, status);

            if (!result.IsSuccess)
            {
                return result.ErrorMessage == "NotFound"
                    ? NotFound("Customer with this Id not found")
                    : BadRequest(result.ErrorMessage);
            }

            return Ok("Customer info updated");
        }

        [HttpDelete("DeleteCustomerId")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (!result)
            {
                return NotFound("Customer with this Id not found");
            }

            return Ok("Customer deleted");
        }
    }
}
