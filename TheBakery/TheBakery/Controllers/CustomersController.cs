using Microsoft.AspNetCore.Mvc;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Services;

namespace TheBakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetCustomer()
        {
            return (await _customerService.GetAllAsync()).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/5/Address
        [HttpGet("{id}/Address")]
        public async Task<ActionResult<Address>> GetCustomerAddress(Guid id)
        {
            var address = await _customerService.GetAddressByCustomerId(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, PutCustomerDto customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var result = await _customerService.UpdateAsync(id, customer);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(PostCustomerDto customer)
        {
            var created = await _customerService.CreateAsync(customer);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Customer.");
            }

            return CreatedAtAction("GetCustomer", new { id = created.Item2?.CustomerId }, created.Item2);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
